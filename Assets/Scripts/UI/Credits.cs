using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;
using TMPro;
using RDCT.Menu.SettingsMenu;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float timeToPressAgain = 5f;
    [SerializeField] private TMP_Text skipCreditsText;
    [SerializeField] private RenderTexture videoRenderTexture;

    private VideoPlayer video;
    private float currentTimeToPress = 0f;
    private bool confirmingToSkip = false;

    private void Awake()
    {
        video = GetComponent<VideoPlayer>();

        var userSettingsJson = File.ReadAllText(Application.persistentDataPath + "/UserSettings.json");
        var userSettingsObject = JsonUtility.FromJson<SettingsStorage>(userSettingsJson);

        mixer.SetFloat("VolumeBGM", userSettingsObject.BGMVolume);

        StartCoroutine("PlayCredits");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SkipCredits();
        }

        if (currentTimeToPress > 0f)
            currentTimeToPress -= Time.deltaTime;
        else
            confirmingToSkip = false;

        skipCreditsText.enabled = confirmingToSkip;
    }

    IEnumerator PlayCredits()
    {
        yield return new WaitForSeconds((float)video.length);

        LoadingScreen.LoadScene("MainMenu 1");
    }

    private void SkipCredits()
    {
        if (!confirmingToSkip)
        {
            currentTimeToPress = timeToPressAgain;
            confirmingToSkip = true;
        }
        else
        {
            StopCoroutine("PlayCredits");
            video.Stop();
            LoadingScreen.LoadScene("MainMenu 1");
        }
    }

    private void OnDestroy()
    {
        videoRenderTexture.Release();
    }
}
