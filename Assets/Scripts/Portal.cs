using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName; // ���� ���� �̸�

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��Ż�� �÷��̾ �浹���� ��
        if (other.CompareTag("Player"))
        {
            // ���� ������ ��ȯ
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
