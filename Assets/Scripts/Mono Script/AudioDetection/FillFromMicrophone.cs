using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillFromMicrophone : MonoBehaviour
{
    public Image audioBar;
    public AudioLoudnessDetector loudnessDetector;

    public float loudnessSensibility = 100f;
    public float threshold = 0.1f;

    private void Update()
    {

        float loudness = loudnessDetector.GetLoudnessFromMicrophone() * loudnessSensibility;
        //Debug.Log(loudness * 50);
        if (loudness < threshold) loudness = 0.01f;

        audioBar.rectTransform.sizeDelta = new Vector2(loudness * 50, audioBar.rectTransform.rect.height);

    }


}
