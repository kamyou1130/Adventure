using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIController : MonoBehaviour
{
    public static DeathUIController Instance; // �̱��� �ν��Ͻ�
    public GameObject deathUIPrefab;  // DeathUI Panel ������ ����

    private string currentThemeFirstScene; // ���� �׸��� ù �� �̸��� ����

    void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ������Ʈ�� �ı�
        }
    }

    // DeathUI Ȱ��ȭ �Լ�
    public void ShowDeathUI(string themeFirstSceneName)
    {
        // Death UI Panel ������ �ν��Ͻ�ȭ
        GameObject deathUIInstance = Instantiate(deathUIPrefab, GameObject.Find("Canvas").transform); // Canvas �ȿ� ����
        deathUIInstance.SetActive(true);  // Death UI Ȱ��ȭ
        currentThemeFirstScene = themeFirstSceneName;  // ���� �׸��� ù �� �̸� ����
    }

    // Restart ��ư�� ������ �� ȣ��
    public void OnRestartButtonPressed()
    {
        // ���� �׸��� ù ������ �����
        Debug.Log("Restarting scene: " + GetThemeStartScene());
        SceneManager.LoadScene(GetThemeStartScene());
    }

    // Exit ��ư�� ������ �� ȣ��
    public void OnExitButtonPressed()
    {
        // �κ� ������ ��ȯ
        SceneManager.LoadScene("Lobby");
    }

    // ���� �׸��� ù �� �̸� ��ȯ
    public string GetThemeStartScene()
    {
        // �׸��� ���� ù �� �̸� ��ȯ
        switch (currentThemeFirstScene)
        {
            case "1-1":
            case "1-2":
            case "1-3":
            case "1-4":
                return "1-1";  // 1�׸��� ���
            case "2-1":
            case "2-2":
            case "2-3":
            case "2-4":
                return "2-1";  // 2�׸��� ���
            case "3-1":
            case "3-2":
            case "3-3":
            case "3-4":
                return "3-1";  // 3�׸��� ���
            case "4-1":
            case "4-2":
            case "4-3":
            case "4-4":
                return "4-1";  // 4�׸��� ���
            default:
                return "1-1";  // �⺻������ 1-1 ������ ����
        }
    }
}
