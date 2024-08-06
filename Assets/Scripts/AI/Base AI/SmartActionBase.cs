using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SAType{
    AUDIO,
    VISUAL,
    ACTION
}

public class SmartActionBase : MonoBehaviour
{
    public SAType type;
}
