using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName ="Attribute",menuName ="AI / General Attribute",order = 0)]
public class GeneralAIStats : ScriptableObject
{
    [Header("Base Attribute")]
    [SerializeField] private string AIName;
    [SerializeField] [TextArea] private string BriefDescription;

    [Header("Movement Option")]
    [SerializeField] private float AIMoveSpeed;
    [SerializeField] private float AITurnRate;
    [SerializeField] private float AIAccelerationSpeed;
    [SerializeField] [Range(1,3)] private float AIChaseSpeed;
    [SerializeField] [Range(1, 3)] private int AIChaseAccelMultiplier;

    [Header("Agent Setting")]
    [SerializeField] private float AgentRadius;
    [SerializeField] private float AgentHeight;
    [SerializeField] private LayerMask AreaMasking;
    [SerializeField] private string PlayerTag;

    [Header("Detection")]
    [SerializeField] private float AIDetectionRange;
    [SerializeField] private float AIVisionRange;
    [SerializeField] private float AIForgetTime;
    [SerializeField] private float AIDelayTime;
    [SerializeField] [Range(1,5)] private int AISoundDetectionRange;
    [SerializeField] [Range(1,5)] private int InvestigateTime;

    [Header("AI Event")]
    [SerializeField][Range(1, 100)] private float AIHuntChances;

    [Header("AI Sound")]
    public AudioClip[] AIIdleSound;
    public AudioClip[] AIRoamSound;
    public AudioClip[] AIChaseSound;
    public AudioClip[] AIHuntSound;

    public float getAgentDetectionRange()
    {
        return AIVisionRange;
    }

    public float getAIVoiceDetectionRange()
    {
        return AISoundDetectionRange;
    }

    public float getForgetTime()
    {
        return AIForgetTime;
    }

    public float getAIChaseSpeed()
    {
        return AIMoveSpeed * AIChaseSpeed;
    }

    public float getAINormalSpeed()
    {
        return AIMoveSpeed;
    }

    public float getAINormalTurnRate()
    {
        return AITurnRate;
    }

    public float getAICHaseTurnRate()
    {
        return AITurnRate * 2;
    }

    public float getAIChaseAcceleration()
    {
        return AIAccelerationSpeed * AIChaseAccelMultiplier;
    }
    
    public float getDefaultAcceleration()
    {
        return AIAccelerationSpeed;
    }

    public int getInvestigateTime()
    {
        return InvestigateTime;
    }

    public float getDelayTime()
    {
        return AIDelayTime;
    }
}
