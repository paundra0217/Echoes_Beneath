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
    [SerializeField] int BanyakPipe;
    [SerializeField] int BanyakGenerator;
    private int GeneratorPoint = 0;
    private int PipePoint = 0;

    bool ObjectivePipe = false;
    bool ObjectiveGenerator = false;

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

}
