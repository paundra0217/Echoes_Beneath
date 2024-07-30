using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIUtility : AIBase
{
    // General Attribute
    [SerializeField] GeneralAIStats aiStats;
    private NavMeshAgent agent;
    private AIFieldOfView perception;
    [SerializeField][Range(.1f, 1.5f)] private float perceptionUpdateTime;
    [SerializeField][Range(.1f, 2f)] private float aiEvaluationTimer;
    [SerializeField] GameObject lastSeenPosObj;
    bool investigate, chase;
    bool resetInvestigate = false;

    public List<GameObject> detectedTarget = new List<GameObject>();
    
    // Smart Action Action
    public List<SmartAction> smartActions = new List<SmartAction>();

    // Collider
    Collider[] coll;

    // Time Refrences
    int Hour, Minute;

    // Player Refrences
    // mPlayer From AIBase
    [HideInInspector] public AudioLoudnessDetector audioLoudnessDetector;
    bool detected = false;
    bool voiceDetected = false;
    List<Vector3> lastSeenPos = new List<Vector3>(); // Player Last Seen Transform Position
    GameObject[] clones;


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
    }

    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        perception = GetComponent<AIFieldOfView>();
        findPlayer();
        StartCoroutine(perceptionUpdate(perceptionUpdateTime));
        StartCoroutine(aiAgentEvaluationTimer(aiEvaluationTimer));
    }

    public override void onSelected()
    {
        isSelected = true;
        base.onSelected();
        playerSound = audioLoudnessDetector.GetLoudnessFromMicrophone() * 100;
        findSmartAction();
        perceptionEvaluation();
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
        }
        else if (voiceDetected)
        {
            if (!investigate)
                StartCoroutine(investiageTimer(5, aiStats.getDelayTime()));
            if (investigate)
            {
                resetInvestigate = true;
                if (lastSeenPos.Count >= 50)
                {
                    for (int i = 0;i < lastSeenPos.Count - 2;i++)
                    {
                        lastSeenPos.RemoveAt(i);
                    }
                }
            }
        }

        state = aiStateEvaluation();
    }

    private void Investigate()
    {
        int keBerapa = lastSeenPos.Count;
        Vector3 pos = lastSeenPos[keBerapa - 1];
        Vector3 invest = new Vector3(pos.x + Random.Range(-5, 5), pos.y, pos.z + Random.Range(-5, 5));

        agent.SetDestination(invest);
        investigate = true;
        return;
    }

    private void Chase()
    {
        agent.SetDestination(mPlayer.transform.position);
        chase = true;
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
                detectedTarget.Remove(obj);
                Debug.Log("Remove " + obj);
                chase = false;
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
            return true;

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
        if (!investigate && !chase) return AIState.IDLE;

        return AIState.IDLE;
    }
}