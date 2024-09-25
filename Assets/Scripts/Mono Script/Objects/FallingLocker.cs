using RDCT.Audio;
using System.Collections;
using UnityEngine;

public class FallingLocker : MonoBehaviour, IObjectEventBase
{
    private Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    public void FireEvent()
    {
        StartCoroutine("LockerFall");
    }

    public void ResetEvent()
    {
        anim.Rewind();
    }

    IEnumerator LockerFall()
    {
        anim.Play();
        yield return new WaitForSeconds(anim.clip.length);

        AudioController.Instance.PlaySFX("LockerFall");
    }
}
