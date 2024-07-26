using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AIUtility : AIBase
{
    // General Attribute
    [SerializeField] GeneralAIStats aiStats;
    
    // Smart Action Action
    private List<SmartAction> smartAction;

    // Collider
    Collider[] coll;

    // Time Refrences
    int Hour, Minute;


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

    private void findSmartAction()
    {
        float searchRadius = aiStats.getAgentDetectionRange();
        coll = Physics.OverlapSphere(transform.position, searchRadius);

        foreach (var SA in coll)
        {
            if (SA.TryGetComponent<SmartAction>(out SmartAction smartA))
            {
                if (smartA == null) return;
                smartAction.Add(smartA);
                smartA.doSmartAction();
            } else return;
        }
    }

    private void OptimizeSAList()
    {
        foreach(SmartAction SA in smartAction)
        {
            var distance = Vector3.Distance(this.transform.position, SA.transform.position);
            if (distance > 20)
            {
                smartAction.Remove(SA);
                return;
            }
            else return;
        }
    }


}
