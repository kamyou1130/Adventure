using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private void Awake()
    {
        // 현재 씬에 Player 오브젝트가 존재하는지 확인하고, 존재하면 삭제
        if (FindObjectsOfType<PlayerController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
