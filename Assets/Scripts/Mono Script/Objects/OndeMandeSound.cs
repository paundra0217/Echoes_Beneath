using RDCT.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndeMandeSound : InteractObject
{
    public override void Interaction()
    {
        AudioController.Instance.PlaySFX("Onde Mande");
    }
}
