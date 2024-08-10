using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    
    [SerializeField] int BanyakPipe;
    [SerializeField] int BanyakGenerator;
    private int GeneratorPoint;
    private int PipePoint;

    bool ObjectivePipe = false;
    bool ObjectiveGenerator = false;

    public void PipeObjectiveClear()
    {
        PipePoint++;

        if(PipePoint >= BanyakPipe)
        {
            ObjectivePipe = true;
        }
    }

    public void GeneratorObjectiveClear()
    {
        GeneratorPoint++;
        if(GeneratorPoint >= BanyakGenerator)
        {
            ObjectiveGenerator = true;
        }
    }

}
