using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] TMP_Text BanyakPipetxt;
    [SerializeField] TMP_Text BanyakGeneratortxt;
    [SerializeField] TMP_Text PipePointxt;
    [SerializeField] TMP_Text GeneratorPointtxt;
    [SerializeField] TMP_Text BanyakFusetxt;
    [SerializeField] TMP_Text BanyakFuseBoxxtx;
    [SerializeField] TMP_Text FusePointtxt;
    [SerializeField] TMP_Text FuseboxPointtxt;
    [SerializeField] int BanyakPipe;
    [SerializeField] int BanyakGenerator;
    [SerializeField] int BanyakFuse;
    [SerializeField] int BanyakFuseBox;
    private int GeneratorPoint = 0;
    private int PipePoint = 0;
    private int FusePoint, FuseBoxPoint;

    private bool ObjectivePipe = false;
    private bool ObjectiveGenerator = false;

    private void Start()
    {
        BanyakGeneratortxt.text = BanyakGenerator.ToString();
        BanyakPipetxt.text = BanyakPipe.ToString();
        PipePointxt.text = PipePoint.ToString();
        GeneratorPointtxt.text = GeneratorPoint.ToString();

    }

    public void PipeObjectiveClear()
    {
        PipePoint++;
        PipePointxt.text = PipePoint.ToString();
        if (PipePoint >= BanyakPipe)
        {
            ObjectivePipe = true;
        }
    }

    public void GeneratorObjectiveClear()
    {
        GeneratorPoint++;
        GeneratorPointtxt.text = GeneratorPoint.ToString();
        if (GeneratorPoint >= BanyakGenerator)
        {
            ObjectiveGenerator = true;
        }
    }

    public bool CheckObjective()
    {
        if(ObjectivePipe && ObjectiveGenerator)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
