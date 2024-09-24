using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;

public class FLight : MonoBehaviour
{
    [SerializeField] GameObject light1, light2;
    [SerializeField] GameObject _object;
    private Material[] objectmats;
    [SerializeField] float minTime,maxTime;
    private Color originEmis;
    private bool startFlicker;
    // Start is called before the first frame update
    void Start()
    {
        objectmats = _object.GetComponent<Renderer>().materials;
        originEmis = objectmats[1].GetColor("_EmissiveColor");
        startFlicker = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlicker)
        {
            StartCoroutine(flicker());
        }
    }

    void Off()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        Color emissiveColor = Color.black;
        objectmats[1].SetColor("_EmissiveColor", emissiveColor);
    }

    void On()
    {
        light1.SetActive(true);
        light2.SetActive(true);
        objectmats[1].SetColor("_EmissiveColor", originEmis);
    }

    IEnumerator flicker()
    {
        startFlicker = false;
        float interval;
        Off();
        interval = Random.Range(minTime,maxTime);
        yield return new WaitForSeconds(interval);
        On();
        interval = Random.Range(minTime,maxTime);
        yield return new WaitForSeconds(interval);
        Off();
        interval = Random.Range(minTime,maxTime);
        yield return new WaitForSeconds(interval);
        On();
        startFlicker = true;
    }

    public void Activate()
    {
        startFlicker = true;
    }

    public void Deactive()
    {
        startFlicker = false;
    }
}
