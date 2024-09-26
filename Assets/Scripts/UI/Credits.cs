using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Credits : MonoBehaviour
{
    private VideoPlayer video;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();

        StartCoroutine("PlayCredits");
    }

    IEnumerator PlayCredits()
    {
        print(video.length);
        yield return new WaitForSeconds((float)video.length);

        LoadingScreen.LoadScene("MainMenu 1");
    }
}
