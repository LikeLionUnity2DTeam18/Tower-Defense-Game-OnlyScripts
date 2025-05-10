using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("설정 패널 (옵션)")]
    public GameObject settingPanel;

    public void OnClickStart()
    {
        SceneManager.LoadScene("3rdTest"); // 실제 게임 씬 이름으로 교체
    }

    public void OnClickSetting()
    {
        if (settingPanel != null)
            settingPanel.SetActive(true);
        else
            Debug.LogWarning("설정 패널이 연결되지 않았습니다.");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}