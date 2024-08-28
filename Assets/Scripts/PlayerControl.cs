using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private void Awake()
    {
        // ���� ���� Player ������Ʈ�� �����ϴ��� Ȯ���ϰ�, �����ϸ� ����
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
