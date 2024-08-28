using UnityEngine;

public class UIControl : MonoBehaviour
{
    private void Awake()
    {
        // ���� ���� UI ������Ʈ�� �����ϴ��� Ȯ���ϰ�, �����ϸ� ����
        if (FindObjectsOfType<UIControl>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
