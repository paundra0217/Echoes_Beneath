using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] List<ObjectiveTask> objectivesTask;
    [SerializeField] private int Index = 0;
    [SerializeField] GameObject Debres;
    [SerializeField] Transform DebresSpawn;
    [SerializeField] Animator animator;
    private void Start()
    {
        foreach (Objective objective in objectivesTask[Index].objectives)
        {
            if (objective.EventTrigger) continue;
            //objective.ObjectiveTxt.gameObject.SetActive(true);
            objective.SetText();
        }
    }

    public void ObjectiveClear(string ObjectiveName)
    {
        bool CekKelar = true;

        foreach(Objective objective in objectivesTask[Index].objectives)
        {
            if(ObjectiveName == objective.Title)
            {
                objective.ObjectiveClearMinigames();
                //return;
            }

        }

        foreach (Objective objective in objectivesTask[Index].objectives)
        {
            if(objective.Title == "JournalPage")
            {
                if (objective.ObjectiveClear)
                {

                    ObjectiveSectionClear();
                    return;
                }
            }

            if (!objective.ObjectiveClear) CekKelar = false;
        }

        
        if (CekKelar)
        {
            ObjectiveSectionClear();

        }
    }

    public void TriggerObjective(string ObjectiveName)
    {
        foreach (Objective objective in objectivesTask[Index].objectives)
        {          
            if (ObjectiveName == objective.Title)
            {
                objective.SetText();
            }

        }
    }

    private void ObjectiveSectionClear()
    {
        objectivesTask[Index].unityEvent.Invoke();
        foreach (Objective objective in objectivesTask[Index].objectives)
        {
            objective.ObjectiveTxt.gameObject.SetActive(false);
        }

        Index++;

        foreach (Objective objective in objectivesTask[Index].objectives)
        {
            if(objective.EventTrigger) continue;
            //objective.ObjectiveTxt.gameObject.SetActive(true);
            objective.SetText();
        }

        /*
        if(Index == 4)
        {
            animator.SetTrigger("Kebuka");
            dialogBase.Instance.panggilDialog("2.9");
        }
        else if(Index == 5)
        {
            FindAnyObjectByType<PlayerMotor>().GetComponent<PlayerMotor>().CanRun = true;
            GameObject oke = Instantiate(Debres, DebresSpawn);
        }
        */
    }

    public int GetIndex()
    {
        return Index;
    }
}

[System.Serializable]
public class ObjectiveTask
{
    public Objective[] objectives;
    public UnityEvent unityEvent;
    public bool Kelar;
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
    public bool EventTrigger;

    public void SetText()
    {

        if(BanyakObjective == 0)
        {

            ObjectiveTxt.text = ObjectiveTask;
        }
        else
        {
          ObjectiveTxt.text = "- " + ObjectiveTask + " (" + BanyakPoin.ToString() + "/" + BanyakObjective.ToString() + ")";
        }
        ObjectiveTxt.gameObject.SetActive(true);
    }

    public void ObjectiveClearMinigames()
    {
        BanyakPoin++;
        ObjectiveTxt.text = "- "+ ObjectiveTask + " (" + BanyakPoin.ToString() + "/" + BanyakObjective.ToString() + ")";

        if (BanyakPoin == BanyakObjective)
        {
            ObjectiveClear = true;
        }
    }



}
