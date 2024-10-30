using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimedRandomSound : MonoBehaviour
{
    [SerializeField] float[] _setOfTime;
    int _randomNumber;
    bool _timerActive, isActive;
    float _currentTime, _timer;
    [SerializeField] AudioClip _waterSplash;
    [SerializeField] AudioSource sfxObject;

    void Start()
    {
        _currentTime = 0;
        _randomNumber = UnityEngine.Random.Range(0, _setOfTime.Length);
        _timer = _setOfTime[_randomNumber];
        _timerActive = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(isActive)    Timer();
    }
        

    void CallSFX()
    {
        //get new random number
        _randomNumber = UnityEngine.Random.Range(0, _setOfTime.Length);
        //reset timer set random number
        _timer = _setOfTime[_randomNumber];
        _currentTime = 0f;
        //play sfx
        PlaySFXClip(_waterSplash, transform, 0.1f);
    }

    void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        Vector3 spawnVector = new Vector3(spawnTransform.position.x, spawnTransform.position.y, spawnTransform.position.z);
        spawnVector = GetRandomPosition(spawnVector);
        AudioSource audioSource = Instantiate(sfxObject, spawnVector, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    Vector3 GetRandomPosition(Vector3 _randomPosition)
    {
        _randomPosition = transform.position;
        _randomPosition = new Vector3 (_randomPosition.x + UnityEngine.Random.Range(-100,100), _randomPosition.y, _randomPosition.z + UnityEngine.Random.Range(-100,100));

        return _randomPosition;
    }

    public void Activate()
    {
        isActive = true;
        _timerActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
        _timerActive = false;
    }

    private void Timer()
    {
        if(_timerActive) _currentTime = _currentTime + Time.deltaTime;
        TimeSpan _actualTime = TimeSpan.FromSeconds(_currentTime);
        if(_actualTime.Seconds == _timer)
        CallSFX();
    }
}
