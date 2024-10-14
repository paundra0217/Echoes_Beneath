using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.Audio;

public class MetalBarTrigger : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
           AudioController.Instance.PlaySFX("Metal Bar Sound");
        }
    }
}
