using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;
using Random = UnityEngine.Random;

public class AIUtility : AIBase
{
    // General Attribute
    [SerializeField] GeneralAIStats aiStats;
    private NavMeshAgent agent;
    private AIFieldOfView perception;
    [SerializeField][Range(.1f, 1.5f)] private float perceptionUpdateTime;
    [SerializeField][Range(.1f, 2f)] private float aiEvaluationTimer;
    //[SerializeField] GameObject lastSeenPosObj;
    bool investigate, chase, roam, isMakingSound;
    bool resetInvestigate = false;
    public AudioSource aSource;
    public Animator animator;
    //Vector3 lastMoveTo;

    public List<AIPOI> AIPois = new List<AIPOI>();

    public List<GameObject> detectedTarget = new List<GameObject>();
    
    // Smart Action Action
    public List<SmartAction> smartActions = new List<SmartAction>();

    // Collider
    Collider[] coll;

    // Time Refrences
    int Hour, Minute;
    int minuteTimeStop, timeElapsed;

    // Player Refrences
    // mPlayer From AIBase
    public AudioLoudnessDetector audioLoudnessDetector;
    bool detected = false;
    bool voiceDetected = false;
    List<Vector3> lastSeenPos = new List<Vector3>(); // Player Last Seen Transform Position
    bool reacting;
    PlayerState pState;
    PlayerMotor motor;
    //GameObject[] clones;


    // Sound Var
    [Header("Sound Setting")]
    private float playerSound;
    [Range(1,10)] public int soundTreshold;


    #region Time Refrences

    private void Awake()
    {
        TimeManager.onMinuteChange += UpdateTime;
        TimeManager.onHourChange += UpdateTime;
    }

    private void UpdateTime()
    {
        Hour = TimeManager.Hour;
        Minute = TimeManager.Minute;
        timeElapsed++;
    }

    #endregion

    override public void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        perception = GetComponent<AIFieldOfView>();
        findPlayer();
        StartCoroutine(perceptionUpdate(perceptionUpdateTime));
        StartCoroutine(aiAgentEvaluationTimer(aiEvaluationTimer));
        aSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        timeElapsed = 0;
        reacting = true;
        motor = mPlayer.GetComponent<PlayerMotor>();
        pState = motor.getPlayerstate();
        StartCoroutine(randomVoice());
        if (AIPois.Count == 0)
        {
            GameObject[] pois;
            pois = GameObject.FindGameObjectsWithTag("AIPoi");
            if (pois != null)
            {
                foreach (var po in pois)
                {
                    AIPOI poi = po.GetComponent<AIPOI>();
                    AIPois.Add(poi);
                }
            } else if (pois == null)
            {
                Debug.Log("PASANG POI AINYA");
            }
        }
    }

    public override void onSelected()
    {
        pState = motor.playerState;
        if (pState == PlayerState.InMinigames)
        {
            playerInMinigames();
            return;
        }
        isSelected = true;
        base.onSelected();
        playerSound = audioLoudnessDetector.GetLoudnessFromMicrophone() * 100;
        findSmartAction();
        perceptionEvaluation();
        animationControllers();
        if (aSource.isPlaying == false)
            isMakingSound = false;
    }

    void playerInMinigames()
    {
        agent.acceleration = 0;
        agent.SetDestination(transform.position);
    }

    void animationControllers()
    {
        if (agent.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void findSmartAction()
    {
        float searchRadius = aiStats.getAgentDetectionRange();
        coll = Physics.OverlapSphere(transform.position, searchRadius);

        OptimizeSAList();

        foreach (var SA in coll)
        {
            if (SA.TryGetComponent(out SmartAction smartA))
            {
                if (smartA == null) return;
                Debug.Log("Ada SA");
                if (smartActions.Contains(smartA)) return;
                smartActions.Add(smartA);
                //smartA.doSmartAction();
            } else return;
        }
    }

    private void OptimizeSAList()
    {
        foreach(SmartAction SA in smartActions)
        {
            float distance = Vector3.Distance(transform.position, SA.getSAPosition());
            if (distance > aiStats.getAgentDetectionRange() * 2)
            {
                smartActions.Remove(SA);
                return;
            }
            else return;
        }
    }

    private void evaluateLoudness(float loudness)
    {
        voiceDetected = false;
        if (loudness < soundTreshold) return;
        Debug.Log("SUARA");
        float distance = Vector3.Distance(transform.position, mPlayer.transform.position);
        if (distance <= aiStats.getAIVoiceDetectionRange())
        {
            voiceDetected = true;
            //clones[1] = (GameObject)Instantiate(lastSeenPosObj, mPlayer.transform.position, Quaternion.identity);

            //Instantiate(lastSeenPosObj);

            Vector3 temp = new Vector3(mPlayer.transform.position.x, mPlayer.transform.position.y, mPlayer.transform.position.z);

            lastSeenPos.Add(temp);
        }   
        else voiceDetected = false;

        if (lastSeenPos.Count >= 100)
        {
            for (int i = 0; i < lastSeenPos.Count - 10; i++)
            {
                lastSeenPos.RemoveAt(i);
            }
        }
    }

    public float getAIFOVRange()
    {
        return aiStats.getAgentDetectionRange();
    }

    public float getForgetTime()
    {
        return aiStats.getForgetTime();
    }

    private void perceptionEvaluation()
    {
        evaluateLoudness(playerSound);
        detected = false;
        for (int i = 0; i < detectedTarget.Count; i++)
        {
            if (detectedTarget.Contains(mPlayer))
            {
                Debug.Log("Ada Player");
                detected = true;
            } else detected = false;
        }

        if (isPlayerOnRange() == true)
        {
            Chase();
            Debug.Log("Player Range");
        }
        else if (voiceDetected)
        {
            if (!investigate)
                StartCoroutine(investiageTimer(15, aiStats.getDelayTime()));
            if (investigate)
            {
                resetInvestigate = true;
                Debug.Log("Reset Investigate");
            }

            Debug.Log("Voice Detected");
        }
        else
        {
            AiRoam();
            Debug.Log("Roam");
        }
        //Debug.Log(agent.velocity.magnitude);
        state = aiStateEvaluation();
    }

    private void Investigate()
    {
        int keBerapa = lastSeenPos.Count - 1;
        int random = 0;
        if (keBerapa > 5) random = Random.Range(1, 3);
        Debug.Log(keBerapa + 1 + " <-  -> " + random + ", Result = " + keBerapa);
        Vector3 pos = lastSeenPos[keBerapa - random];
        Vector3 invest = new Vector3(pos.x + Random.Range(-5, 5), pos.y, pos.z + Random.Range(-5, 5));

        agent.SetDestination(invest);
        investigate = true;
        roam = false;
        return;
    }

    private void Chase()
    {
        agent.SetDestination(mPlayer.transform.position);
        JumpscareHandler();
        chase = true;
        roam = false;
    }

    IEnumerator randomVoice()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
            playRandomSound();
        }
    }

    void playRandomSound()
    {
        switch (state)
        {
            case AIState.IDLE:
                {
                    AudioClip clip = aiStats.AIIdleSound[Random.Range(0, aiStats.AIIdleSound.Length - 1)];
                    aSource.PlayOneShot(clip);
                    break;
                }
            case AIState.ROAM:
                {
                    AudioClip clip = aiStats.AIIdleSound[Random.Range(0, aiStats.AIIdleSound.Length - 1)];
                    aSource.PlayOneShot(clip);
                    break;
                }
            case AIState.INVESTIGATE:
                {
                    AudioClip clip = aiStats.AIIdleSound[Random.Range(0, aiStats.AIIdleSound.Length - 1)];
                    aSource.PlayOneShot(clip);
                    break;
                }
            case AIState.CHASE:
                {
                    break;
                }
            case AIState.HUNT:
                {
                    break;
                }
        }
    }

    private void AiRoam()
    {
        List<Vector3> prio = new List<Vector3>();

        foreach(var poi in AIPois)
        {
            prio.Add(poi.getPOIPosition());
        }

        Vector3 goTo = aiRoamPos(prio[Random.Range(0, prio.Count)]);

        if (agent.velocity.magnitude <= 0 && timeElapsed > minuteTimeStop) 
        {
            minuteTimeStop = Random.Range(15,20);
            agent.SetDestination(goTo);
            roam = true;
            timeElapsed = 0;
        }  
    }

    private Vector3 aiRoamPos(Vector3 pos)
    {
        int randoms = Random.Range(-5, 5);
        Vector3 goTo = new Vector3(pos.x + randoms, pos.y, pos.z + randoms);
        
        return goTo;
    }

    public void addDetectedTarget(GameObject obj)
    {
        if (!detectedTarget.Contains(obj))
            detectedTarget.Add(obj);
    }

    void detectedTargetEvaluation()
    {
        for(int i = 0;i < detectedTarget.Count;i++)
        {
            Vector3 target = detectedTarget[i].transform.position;
            float dist = Vector3.Distance(transform.position, target);
            if (dist > aiStats.getAgentDetectionRange())
            {
                StartCoroutine(forgetTime(detectedTarget[i]));
            }
            else
            {
                StopCoroutine(forgetTime(detectedTarget[i]));
                Debug.Log("Timer Reset");
            }
        }
    }

    void stopChase(GameObject obj)
    {
        Vector3 lastPs = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);
        lastSeenPos.Add(lastPs);
        detectedTarget.Remove(obj);
        Debug.Log("Remove " + obj);
        chase = false;
        if (investigate) resetInvestigate = true;
    }

    IEnumerator perceptionUpdate(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            detectedTargetEvaluation();
        }
    }

    IEnumerator forgetTime(GameObject obj)
    {
        int timer = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);
            timer++;
            Debug.Log(timer);
            if (timer >= getForgetTime())
            {
                reacting = true;
                stopChase(obj);
                break;
            }
        }
    }

    IEnumerator investiageTimer(float delay, float reactionTime)
    {
        int investigates = 0;
        while(true)
        {
            yield return new WaitForSeconds(reactionTime);
            if (resetInvestigate == true) investigates = 0;
            Investigate();
            yield return new WaitForSeconds(delay);
            resetInvestigate = false;
            investigates++;
            Debug.Log("Invesitage : " + investigates);
            if (investigates >= delay * aiStats.getInvestigateTime())
            {
                investigate = false;
                lastSeenPos.Clear();
                break;
            }
        }
    }

    public bool isPlayerOnRange()
    {
        if (detected)
        {
            if (!isMakingSound && reacting)
            {
                var klip = aiStats.getRandomReactionSound();
                aSource.PlayOneShot(klip);
                isMakingSound = true;
                reacting = false;
            }
            return true;
        }
            
        if (voiceDetected) return false;

        return false;
    }

    #region AI MOVEMENT HANDLER
    public float moveSpeedHandler()
    {
        switch (state)
        {
            case AIState.IDLE:
                {
                    return aiStats.getAINormalSpeed();
                }

            case AIState.ROAM:
                {
                    return aiStats.getAINormalSpeed();
                }

            case AIState.INVESTIGATE:
                {
                    return aiStats.getAINormalSpeed();
                }

            case AIState.CHASE:
                {
                    return aiStats.getAIChaseSpeed();
                }

            case AIState.HUNT:
                {
                    return aiStats.getAIChaseSpeed();
                }
        }

        return aiStats.getAINormalSpeed();
    }

    public float turnRateHandler()
    {
        switch (state)
        {
            case AIState.IDLE:
                {
                    return aiStats.getAINormalTurnRate();
                }

            case AIState.ROAM:
                {
                    return aiStats.getAINormalTurnRate();
                }

            case AIState.INVESTIGATE:
                {
                    return aiStats.getAINormalTurnRate();
                }

            case AIState.CHASE:
                {
                    return aiStats.getAICHaseTurnRate();
                }

            case AIState.HUNT:
                {
                    return aiStats.getAICHaseTurnRate();
                }
        }

        return aiStats.getAINormalTurnRate();
    }

    public float accelerationHandler()
    {
        switch (state)
        {
            case AIState.IDLE:
                {
                    return aiStats.getDefaultAcceleration();
                }

            case AIState.ROAM:
                {
                    return aiStats.getDefaultAcceleration();
                }

            case AIState.INVESTIGATE:
                {
                    return aiStats.getDefaultAcceleration();
                }

            case AIState.CHASE:
                {
                    return aiStats.getAIChaseAcceleration();
                }

            case AIState.HUNT:
                {
                    return aiStats.getAIChaseAcceleration();
                }
        }

        return aiStats.getDefaultAcceleration();
    }

    #endregion

    IEnumerator aiAgentEvaluationTimer(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            aiAgentEvaluation();
        }
    }

    public void aiAgentEvaluation()
    {
        agent.acceleration = accelerationHandler();
        agent.speed = moveSpeedHandler();
        agent.angularSpeed = turnRateHandler();
    }

    private AIState aiStateEvaluation()
    {
        if (chase) return AIState.CHASE;
        if (investigate) return AIState.INVESTIGATE;
        if (!investigate && !chase) return AIState.ROAM;
        if (agent.velocity.magnitude <= 0) return AIState.IDLE;

        return AIState.IDLE;
    }

    void JumpscareHandler()
    {
        
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.acceleration = 0;
            agent.SetDestination(transform.position);
            animator.SetTrigger("Jumpscare");
            Debug.Log("Jump Scare");
        }
    }

    public void ChangeAfterJumpScare()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

}