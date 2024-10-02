using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool Kunci = false;
    [SerializeField] Transform PusatRotasi;
    //[SerializeField] Transform ParTrans;
    [SerializeField] float PosisiAtas;
    [SerializeField] float PosisiBawah;

    private void Start()
    {
        //ParTrans = GetComponentInParent<Transform>();
        PusatRotasi.localEulerAngles = new Vector3(PosisiAtas, 0, 0);
    }

    private void OnMouseDown() 
    {
        Debug.Log(Kunci);
        Kunci = !Kunci;
        Swap();
    }

    private void Swap()
    {
        if (Kunci)
        {
            //PusatRotasi.rotation = Quaternion.Euler(PosisiBawah, -ParTrans.transform.rotation.y, -ParTrans.transform.rotation.z);
            PusatRotasi.localEulerAngles = new Vector3(PosisiBawah, 0, 0);
        }
        else
        {
            PusatRotasi.localEulerAngles = new Vector3(PosisiAtas, 0, 0);
            //PusatRotasi.rotation = Quaternion.Euler(PosisiAtas, -ParTrans.transform.rotation.y, -ParTrans.transform.rotation.z);
        }
    }

}
