using RDCT.Menu;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int lastFrameIdx;
    private float[] frameDeltaTime;

    public TextMeshProUGUI uiText;

    // Start is called before the first frame update
    void Awake()
    {
        frameDeltaTime = new float[100];
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;

        frameDeltaTime[lastFrameIdx] = Time.deltaTime;
        lastFrameIdx = (lastFrameIdx + 1) % frameDeltaTime.Length;

        uiText.text = Mathf.RoundToInt(calculateFPS()).ToString();
    }

    private float calculateFPS()
    {
        float total = 0;
        foreach (var frame in frameDeltaTime)
        {
            total += frame;
        }

        float fps = Mathf.Clamp(frameDeltaTime.Length / total, 0, 1000);

        return fps;
    }
}
