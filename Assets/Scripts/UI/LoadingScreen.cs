using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDCT.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour, IMenuWindow
{
    [SerializeField] private TMP_Text loadingText;

    private CanvasGroup cg;
    private static LoadingScreen _instance;

    private void Awake()
    {
        _instance = this;
        cg = GetComponent<CanvasGroup>();
    }

    public static void LoadScene(string sceneName)
    {
        _instance.OpenWindow();

        AudioController.Instance.StopBGM();

        _instance.StartCoroutine(_instance.LoadLevelAsync(sceneName));
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName);

        while (!loadOp.isDone)
        {
            int progressValue = (int)(Mathf.Clamp01(loadOp.progress / 0.9f) * 100);
            loadingText.text = progressValue.ToString() + "%";    
            yield return null;
        }
    }

    public void OpenWindow()
    {
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void CloseWindow()
    {
        throw new System.NotImplementedException();
    }
}
