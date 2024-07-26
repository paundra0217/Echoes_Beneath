using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState{
    IDLE,
    ROAM,
    CHASE,
    HUNT
}

public class AIBase : MonoBehaviour
{
    void Update()
    {
        onSelected();
    }

    public virtual void onSelected() { }
    public virtual void onExit() { Destroy(this); }
}
