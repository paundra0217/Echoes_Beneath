using RDCT.Audio;
using System.Collections;
using UnityEngine;

public class FallingLocker : MonoBehaviour, IObjectEventBase
{
    [SerializeField] Collider collider;
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
        dialogBase.Instance.panggilDialog("11");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collider) return;

        if (other.GetComponent<PlayerMotor>())
        {
            FireEvent();
            
        }

    }

}
