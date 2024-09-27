using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] TMP_Text BanyakPipetxt;
    [SerializeField] TMP_Text BanyakGeneratortxt;
    [SerializeField] TMP_Text BanyakFusetxt;
    [SerializeField] TMP_Text BanyakFusePicktxt;

    [SerializeField] TMP_Text PipePointxt;
    [SerializeField] TMP_Text GeneratorPointtxt;
    [SerializeField] TMP_Text FusePointtxt;
    [SerializeField] TMP_Text PickFusePointtxt;

    [SerializeField] int BanyakPipe;
    [SerializeField] int BanyakGenerator;
    [SerializeField] int BanyakFuse = 1;
    [SerializeField] int BanyakFusePick = 3;

    private int GeneratorPoint = 0;
    private int PipePoint = 0;
    private int FusePoint = 0;
    private int PickFusePoint = 0;


    private bool ObjectivePipe = false;
    private bool ObjectiveGenerator = false;
    private bool ObjectiveFuse;

    private void Start()
    {
        BanyakGeneratortxt.text = BanyakGenerator.ToString();
        BanyakPipetxt.text = BanyakPipe.ToString();
        PipePointxt.text = PipePoint.ToString();
        GeneratorPointtxt.text = GeneratorPoint.ToString();

        BanyakFusePicktxt.text = BanyakFusePick.ToString();

        BanyakFusetxt.text = BanyakFuse.ToString();


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

    public void FuseObjectiveClear()
    {
        FusePoint++;
        FusePointtxt.text = FusePoint.ToString();
        if (FusePoint >= BanyakFuse)
        {
            ObjectiveFuse = true;
        }
    }

    public void PickFuseObjectiveClear()
    {
        PickFusePoint++;
        PickFusePointtxt.text = PickFusePoint.ToString();
    }

    public bool CheckObjective()
    {
        if(ObjectivePipe && ObjectiveGenerator && ObjectiveFuse)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
