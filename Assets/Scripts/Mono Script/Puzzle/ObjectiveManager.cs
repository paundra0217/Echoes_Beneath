using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] Objective[] objectives;
    [SerializeField] private int Index = 4;
    private void Awake()
    {
        foreach(Objective objective in objectives)
        {
            objective.SetText();
        }

    }

    public void ObjectiveClear(string ObjectiveName)
    {
        foreach(Objective objective in objectives)
        {
            if(ObjectiveName == objective.Title)
            {
                objective.ObjectiveClearMinigames();
                if (objective.ObjectiveClear)
                {
                    Index++;
                    objectives[Index].ObjectiveTxt = objective.ObjectiveTxt;
                    objectives[Index].ObjectiveTxt.text = "- " + objectives[Index].ObjectiveTask + " (" + objectives[Index].BanyakPoin.ToString() + "/" + objectives[Index].BanyakObjective.ToString() + ")";

                    objective.ObjectiveTxt = null;
                }
                return;
            }
        }
    }


}

[System.Serializable]
public class Objective
{
    public string Title;
    public string ObjectiveTask;
    public TMP_Text ObjectiveTxt;

    public int BanyakObjective;
    public int BanyakPoin;
    public bool ObjectiveClear;

    public void SetText()
    {
        //biar inget bikin kaya gini string temp = "waaiwjdawi" + BanyakObjective.ToString() + " / " + 
        ObjectiveTxt.text = "- " + ObjectiveTask + " (" + BanyakPoin.ToString() + "/" + BanyakObjective.ToString() + ")";
        //BanyakObjectivetxt.text = BanyakObjective.ToString();
        //BanyakPointtxt.text = BanyakPoin.ToString();
    }

    public void ObjectiveClearMinigames()
    {
        BanyakPoin++;
        ObjectiveTxt.text = ObjectiveTask + " (" + BanyakPoin.ToString() + "/" + BanyakObjective.ToString() + ")";

        if (BanyakPoin == BanyakObjective)
        {
            ObjectiveClear = true;
        }
    }

}
