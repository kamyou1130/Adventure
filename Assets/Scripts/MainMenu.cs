using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject SettingPanel; // ���� ���� �г��� ����
    public GameObject explanationPanel; // ���� ���� �г��� ����

    // Start ��ư�� ������ �� ����Ǵ� �Լ�
    public void StartGame()
    {
        // ù ��° ���� ���� �ε� (��: "1-1" ��)
        SceneManager.LoadScene("1-1");
    }

    // ȯ�漳�� ��ư�� ������ �� ����Ǵ� �Լ� (�߰� ���)
    public void ShowSettingPanel()
    {
        SettingPanel.SetActive(true); // ���� �ɼ� �г� Ȱ��ȭ
    }

    public void HideSettingPanel()
    {
        SettingPanel.SetActive(false); // �ɼ� �г� �����
    }

    public void ShowExplanationPanel()
    {
        explanationPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
    }

    public void HideExplanationPanel()
    {
        explanationPanel.SetActive(false); // ���� �г� �����
    }

    public void ExitGame()
    {
        Application.Quit(); // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���� �� ����
#endif
    }

}
