using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartAction : SmartActionBase
{
    // To-Do Smart Action, Actions Inhertiance

    public virtual void doSmartAction() { }

    public virtual Vector3 getSAPosition()
    {
        return this.transform.position;
    }
}
