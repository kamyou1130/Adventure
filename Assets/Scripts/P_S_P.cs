using UnityEngine;

public class P_S_P : MonoBehaviour
{
    // ���� ��ġ�� �����ϱ� ���� Transform�� public���� ����
    public Transform startPosition;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && startPosition != null)
        {
            player.transform.position = startPosition.position; // startPosition�� ��ġ�� �̵�
        }
        else
        {
            Debug.LogWarning("Player object with 'Player' tag not found in the scene or startPosition is null.");
        }
    }
}
