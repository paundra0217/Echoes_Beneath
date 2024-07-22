using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip microphoneClip;
    string MicrophoneName;
    private void Start()
    {
        MicrophoneToAudioClip(0);
    }

    private void Update()
    {
        float Loudness = GetLoudnessFromMicrophone() * 100;
        Debug.Log(Loudness);
    }
    private void MicrophoneToAudioClip(int microphoneIndex)
    {
        //buat cek mic
        /*
        foreach(var name in Microphone.devices)
        {
            Debug.Log(name);
        }
        */
        MicrophoneName = Microphone.devices[microphoneIndex];
        microphoneClip = Microphone.Start(MicrophoneName, true, 20, AudioSettings.outputSampleRate);
    }

    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(MicrophoneName), microphoneClip);
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;
        
        if(startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;
        foreach(var samplefloat in waveData)
        {
            totalLoudness += Mathf.Abs(samplefloat); 
        }

        return totalLoudness / sampleWindow;

    }


}
