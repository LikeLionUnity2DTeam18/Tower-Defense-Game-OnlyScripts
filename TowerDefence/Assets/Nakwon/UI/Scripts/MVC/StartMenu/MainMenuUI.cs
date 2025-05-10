using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    [Header("페이더 설정")]
    public ScreenFader fader;

    [Header("설정 패널 (옵션)")]
    public GameObject settingPannel;

    public void OnClickStart()
    {
        StartCoroutine(LoadSceneWithFade());
    }

    public void OnClickSetting()
    {
        if (settingPannel != null)
            ToggleSetting();
        else
            Debug.LogWarning("설정 패널이 연결되지 않았습니다.");
    }

    public void ToggleSetting()
    {
        settingPannel.SetActive(!settingPannel.activeSelf);
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator LoadSceneWithFade()
    {
        yield return StartCoroutine(fader.FadeOut(1f));
        UnityEngine.SceneManagement.SceneManager.LoadScene("3rdTest");
    }
}