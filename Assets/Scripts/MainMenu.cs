using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingPanel; // 게임 설명 패널을 참조
    public GameObject explanationPanel; // 게임 설명 패널을 참조

    // Start 버튼을 눌렀을 때 실행되는 함수
    public void StartGame()
    {
        // 첫 번째 게임 씬을 로드 (예: "1-1" 씬)
        SceneManager.LoadScene("1-1");
    }

    // 환경설정 버튼을 눌렀을 때 실행되는 함수 (추가 기능)
    public void ShowSettingPanel()
    {
        SettingPanel.SetActive(true); // 게임 옵션 패널 활성화
    }

    public void HideSettingPanel()
    {
        SettingPanel.SetActive(false); // 옵션 패널 숨기기
    }

    public void ShowExplanationPanel()
    {
        explanationPanel.SetActive(true); // 게임 설명 패널 활성화
    }

    public void HideExplanationPanel()
    {
        explanationPanel.SetActive(false); // 설명 패널 숨기기
    }

    public void ExitGame()
    {
        Application.Quit(); // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중일 때 멈춤
#endif
    }

}
