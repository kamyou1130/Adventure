using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIController : MonoBehaviour
{
    public static DeathUIController Instance; // 싱글톤 인스턴스
    public GameObject deathUIPrefab;  // DeathUI Panel 프리팹 참조

    private string currentThemeFirstScene; // 현재 테마의 첫 씬 이름을 저장

    void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 존재하면 새로 생성된 오브젝트는 파괴
        }
    }

    // DeathUI 활성화 함수
    public void ShowDeathUI(string themeFirstSceneName)
    {
        // Death UI Panel 프리팹 인스턴스화
        GameObject deathUIInstance = Instantiate(deathUIPrefab, GameObject.Find("Canvas").transform); // Canvas 안에 생성
        deathUIInstance.SetActive(true);  // Death UI 활성화
        currentThemeFirstScene = themeFirstSceneName;  // 현재 테마의 첫 씬 이름 저장
    }

    // Restart 버튼이 눌렸을 때 호출
    public void OnRestartButtonPressed()
    {
        // 현재 테마의 첫 씬으로 재시작
        Debug.Log("Restarting scene: " + GetThemeStartScene());
        SceneManager.LoadScene(GetThemeStartScene());
    }

    // Exit 버튼이 눌렸을 때 호출
    public void OnExitButtonPressed()
    {
        // 로비 씬으로 전환
        SceneManager.LoadScene("Lobby");
    }

    // 현재 테마의 첫 씬 이름 반환
    public string GetThemeStartScene()
    {
        // 테마에 따라 첫 씬 이름 반환
        switch (currentThemeFirstScene)
        {
            case "1-1":
            case "1-2":
            case "1-3":
            case "1-4":
                return "1-1";  // 1테마일 경우
            case "2-1":
            case "2-2":
            case "2-3":
            case "2-4":
                return "2-1";  // 2테마일 경우
            case "3-1":
            case "3-2":
            case "3-3":
            case "3-4":
                return "3-1";  // 3테마일 경우
            case "4-1":
            case "4-2":
            case "4-3":
            case "4-4":
                return "4-1";  // 4테마일 경우
            default:
                return "1-1";  // 기본적으로 1-1 씬으로 설정
        }
    }
}
