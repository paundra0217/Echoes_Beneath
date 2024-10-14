using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : InteractObject
{
    [SerializeField] private GameObject nota;
    [SerializeField] private int Index;

    public override void Interaction()
    {
        FindAnyObjectByType<NoteManager>().GetComponent<NoteManager>().SetGambar(Index);
        nota.SetActive(true);
    }


}
