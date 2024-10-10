using UnityEngine;

public class Projectile_boss4 : MonoBehaviour
{
    public int damage = 2; // 투사체 데미지
    public float speed = 5f; // 투사체 속도
    private Vector3 direction; // 투사체가 날아갈 방향
    private GameObject boss; // 보스 오브젝트 참조
    private BossHealth bossHealth; // 보스의 체력 스크립트 참조

    // 투사체 초기화 함수
    public void Initialize(Vector3 directionToPlayer, float projectileSpeed)
    {
        direction = directionToPlayer;
        speed = projectileSpeed;

        // 보스 오브젝트와 그 체력 스크립트를 참조
        boss = GameObject.FindGameObjectWithTag("Monster");
        if (boss != null)
        {
            bossHealth = boss.GetComponent<BossHealth>();
            // 보스의 사망 이벤트 구독
            bossHealth.OnBossDeath += HandleBossDeath;
        }
    }

    void Update()
    {
        // 투사체 이동
        transform.position += direction * speed * Time.deltaTime;

        // 보스의 HP가 0이 되면 투사체를 파괴
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Wall 태그를 가진 오브젝트와만 충돌 처리
        if (other.CompareTag("Wall"))
        {
            Vector2 normal;

            // 벽의 이름이나 위치 등을 통해 수직/수평 벽을 구분
            if (other.gameObject.name.Contains("VerticalWall")) // 수직 벽 (왼쪽, 오른쪽 벽)
            {
                normal = Vector2.right; // 수평 방향에서의 반사 처리
            }
            else if (other.gameObject.name.Contains("HorizontalWall")) // 수평 벽 (위, 아래 벽)
            {
                normal = Vector2.up; // 수직 방향에서의 반사 처리
            }
            else
            {
                normal = other.transform.up; // 기본적으로 벽의 Up 벡터 사용
            }

            // 반사 처리
            Vector2 reflectDir = Vector2.Reflect(direction, normal).normalized;
            direction = new Vector3(reflectDir.x, reflectDir.y, 0);
        }

        // 충돌한 오브젝트가 플레이어일 경우
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // 보스가 사망할 때 호출되는 메서드
    private void HandleBossDeath()
    {
        Destroy(gameObject); // 보스가 사망하면 투사체를 삭제
    }
}
