using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPOI : POI
{
    [Range(1, 10)] public int POIPriorityScore;

    private void Start()
    {
        
    }

    public int getPriority()
    {
        return POIPriorityScore;
    }

    public Vector3 getPOIPosition()
    {
        return transform.position;
    }
}
