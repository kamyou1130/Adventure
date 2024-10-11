using UnityEngine;

public class P_S_P : MonoBehaviour
{
    // 시작 위치를 지정하기 위한 Transform을 public으로 설정
    public Transform startPosition;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && startPosition != null)
        {
            player.transform.position = startPosition.position; // startPosition의 위치로 이동
        }
        else
        {
            Debug.LogWarning("Player object with 'Player' tag not found in the scene or startPosition is null.");
        }
    }
}
