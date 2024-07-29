using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class AIUtility : AIBase
{
    // General Attribute
    [SerializeField] GeneralAIStats aiStats;
    private NavMeshAgent agent;
    private AIFieldOfView perception;
    [SerializeField] [Range(.1f,1.5f)] private float perceptionUpdateTime;

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
        StartCoroutine("perceptionUpdate", perceptionUpdateTime);
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
        if (distance < aiStats.getAIVoiceDetectionRange())
            voiceDetected = true;
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

        if (isPlayerOnRange() == true) Chase();
        else state = AIState.IDLE;
    }

    private void Chase()
    {
        agent.SetDestination(mPlayer.transform.position);
        state = AIState.CHASE;
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
            if (timer > getForgetTime())
            {
                detectedTarget.Remove(obj);
                Debug.Log("Remove " + obj);
                break;
            }
        }
    }

    public bool isPlayerOnRange()
    {
        if (detected)
            return true;

        if (voiceDetected) return true;

        return false;
    }
}
