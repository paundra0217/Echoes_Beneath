using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetector : MonoBehaviour
{
    [SerializeField] private SOSettings userSettings;
    public int sampleWindow = 64;

    private AudioClip microphoneClip;
    private string MicrophoneName;

    private void Start()
    {
        //pake mic yang pertama
        //MicrophoneToAudioClip(0);
    }

    #region VoiceDetection
    private void MicrophoneToAudioClip(int microphoneIndex)
    {
        //buat cek nama mic yang tersedia
        
        foreach(var name in Microphone.devices)
        {
            Debug.Log(name);
        }
        
        //Simpen nama Mic yang bakal di pake
        //MicrophoneName = Microphone.devices[microphoneIndex];

        //Mulai Ngedeteksi Suara
        microphoneClip = Microphone.Start(userSettings.inputDevice, true, 20, AudioSettings.outputSampleRate);
    }

    //buat ngereturn Loudness dari mic
    public float GetLoudnessFromMicrophone()
    {
        return GetLoudnessFromAudioClip(Microphone.GetPosition(MicrophoneName), microphoneClip);
    }

    //buat ngereturn Loudness dari AudioClip
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        //Cari posisi Start
        int startPosition = clipPosition - sampleWindow;
        
        //Biar StartPosition kalo minus startnya di 0
        if(startPosition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        //Totalin Loudness
        foreach(var samplefloat in waveData)
        {
            totalLoudness += Mathf.Abs(samplefloat); 
        }

        return totalLoudness / sampleWindow;

    }
    #endregion

}
