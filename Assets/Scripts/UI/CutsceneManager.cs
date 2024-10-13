using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[System.Serializable]
public class CutsceneItem
{
    public string cutsceneName;
    public VideoClip cutsceneClip;
    public UnityEvent eventsBeforeThisCutsceneStart;
    public UnityEvent eventsAfterThisCutsceneEnds;
    public bool onlyPlayOnce;
    [HideInInspector] public bool isAlreadyPlayed;
}

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private CutsceneItem[] cutscenes;
    [SerializeField] private RenderTexture videoRenderTexture;
    [SerializeField] private UnityEvent generalEventsBeforeCutsceneStart;
    [SerializeField] private UnityEvent generalEventsAfterCutsceneEnds;

    private static GameObject cutsceneObject;
    private static CutsceneItem selectedCutscene;
    private static VideoPlayer video;
    private static AudioSource audioSource;

    private static CutsceneManager _instance;

    private void Awake()
    {
        _instance = this;

        cutsceneObject = transform.GetChild(0).gameObject;

        video = cutsceneObject.GetComponent<VideoPlayer>();
        audioSource = cutsceneObject.GetComponent<AudioSource>();

        cutsceneObject.SetActive(false);
    }

    public static void PlayCutscene(string cutsceneName)
    {
        selectedCutscene = _instance.cutscenes.FirstOrDefault(e => e.cutsceneName == cutsceneName);
        if (selectedCutscene == null)
        {
            Debug.LogWarningFormat("Cutscene \"{0}\" does not exist, please double check the name", cutsceneName);
            return;
        }

        if (selectedCutscene.onlyPlayOnce)
        {
            if (selectedCutscene.isAlreadyPlayed)
                return;
            else
                selectedCutscene.isAlreadyPlayed = true;
        }

        cutsceneObject.SetActive(true);

        video.clip = selectedCutscene.cutsceneClip;
        video.SetTargetAudioSource(0, audioSource);

        _instance.generalEventsBeforeCutsceneStart?.Invoke();
        selectedCutscene.eventsBeforeThisCutsceneStart?.Invoke();

        _instance.StartCoroutine("PlayCutsceneUntilEnd");
    }

    public static void EndCutscene()
    {
        video.Stop();

        video.clip = null;
        _instance.videoRenderTexture.Release();
        
        selectedCutscene.eventsAfterThisCutsceneEnds?.Invoke();
        _instance.generalEventsAfterCutsceneEnds?.Invoke();

        cutsceneObject.SetActive(false);
    }

    private IEnumerator PlayCutsceneUntilEnd()
    {
        video.Play();

        yield return new WaitForSeconds((float)video.length);

        EndCutscene();
    }
}
