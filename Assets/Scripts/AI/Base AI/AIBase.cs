using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState{
    IDLE,
    ROAM,
    INVESTIGATE,
    CHASE,
    HUNT
}

public class AIBase : MonoBehaviour
{
    public GameObject mPlayer;
    public AIState state;
    [HideInInspector] public bool isSelected;

    private void Awake()
    {
        onInit();
    }

    public virtual void Start()
    {
        StartCoroutine(onSelectedUpdate());
    }

    public virtual void onSelected() 
    {
        if (!isSelected) return;
    }

    public virtual IEnumerator onSelectedUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(.05f);
            onSelected();
        }
    }
    public virtual void onExit() { Destroy(this); }
    public virtual void findPlayer() { mPlayer = GameObject.FindGameObjectWithTag("Player"); }
    public virtual void onInit() { }
}
