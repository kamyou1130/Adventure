using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    public int playerHealth; // 저장할 체력 정보

    private void Awake()
    {
        // 싱글턴 패턴으로 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않음
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스가 있을 경우 삭제
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
