using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimedRandomSound : MonoBehaviour
{
    float[] _setOfTime = {2, 3, 5};
    int _randomNumber;
    bool _timerActive;
    float _currentTime, _timer;
    [SerializeField] AudioClip _waterSplash;
    Vector3 _randomPosition;

    void Start()
    {
        _currentTime = 0;
        _randomNumber = UnityEngine.Random.Range(0, _setOfTime.Length);
        _timer = _setOfTime[_randomNumber];
        _timerActive = true;
        SFXSingleton.instance.Test();
    }

    // Update is called once per frame
    void Update()
    {
        // if(_timerActive) _currentTime = _currentTime + Time.deltaTime;
        // TimeSpan _actualTime = TimeSpan.FromSeconds(_currentTime);
        // Debug.Log(_actualTime.Seconds);
        // if(_actualTime.Seconds == _timer)
        // {
        //     Debug.Log("IT WORK");
        //     CallSFX();
        // }
    }

    void CallSFX()
    {
        //get new random number
        _randomNumber = UnityEngine.Random.Range(0, _setOfTime.Length);
        //reset timer set random number
        _timer = _setOfTime[_randomNumber];
        _currentTime = 0f;
        //play sfx
        SFXSingleton.instance.PlaySFXClip(_waterSplash, transform, 1f);
    }

    void GetRandomPosition()
    {

    }

}
