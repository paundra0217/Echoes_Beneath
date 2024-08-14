using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool Kunci = false;
    [SerializeField] Transform PusatRotasi;
    [SerializeField] float PosisiAtas;
    [SerializeField] float PosisiBawah;


    private void FixedUpdate()
    {
        if (Kunci)
        {
            PusatRotasi.rotation = Quaternion.Euler(PosisiBawah, 0f, 0f);
        }
        else
        {
            PusatRotasi.rotation = Quaternion.Euler(PosisiAtas, 0f, 0f);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(Kunci);
        Kunci = !Kunci;
    }


}
