using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.Audio;

public class PlayerSFX : MonoBehaviour
{
    public void PlayWalkSound()
    {
        AudioController.Instance.PlaySFX("PlayerWalk");
    }

}
