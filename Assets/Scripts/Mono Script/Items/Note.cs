using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : InteractObject
{
    [SerializeField] private int Index;

    public override void Interaction()
    {
        FindAnyObjectByType<NoteManager>().GetComponent<NoteManager>().UnlockPage(Index);
        Destroy(gameObject);
        //nota.SetActive(true);
    }


}
