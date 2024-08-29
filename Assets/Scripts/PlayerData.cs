using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public int playerHealth; // ������ ü�� ����

    private void Awake()
    {
        // �̱��� �������� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ��� ���� ��� ����
        }
    }

    public void SavePlayerHealth(int health)
    {
        playerHealth = health;
    }

    public int LoadPlayerHealth()
    {
        return playerHealth;
    }
}
