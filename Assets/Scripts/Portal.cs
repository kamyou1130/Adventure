using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName; // 다음 씬의 이름

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 포탈에 플레이어가 충돌했을 때
        if (other.CompareTag("Player"))
        {
            // 다음 씬으로 전환
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
