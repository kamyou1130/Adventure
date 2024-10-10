using UnityEngine;

public class Projectile_boss4 : MonoBehaviour
{
    public int damage = 2; // ����ü ������
    public float speed = 5f; // ����ü �ӵ�
    private Vector3 direction; // ����ü�� ���ư� ����
    private GameObject boss; // ���� ������Ʈ ����
    private BossHealth bossHealth; // ������ ü�� ��ũ��Ʈ ����

    // ����ü �ʱ�ȭ �Լ�
    public void Initialize(Vector3 directionToPlayer, float projectileSpeed)
    {
        direction = directionToPlayer;
        speed = projectileSpeed;

        // ���� ������Ʈ�� �� ü�� ��ũ��Ʈ�� ����
        boss = GameObject.FindGameObjectWithTag("Monster");
        if (boss != null)
        {
            bossHealth = boss.GetComponent<BossHealth>();
            // ������ ��� �̺�Ʈ ����
            bossHealth.OnBossDeath += HandleBossDeath;
        }
    }

    void Update()
    {
        // ����ü �̵�
        transform.position += direction * speed * Time.deltaTime;

        // ������ HP�� 0�� �Ǹ� ����ü�� �ı�
        if (bossHealth != null && bossHealth.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Wall �±׸� ���� ������Ʈ�͸� �浹 ó��
        if (other.CompareTag("Wall"))
        {
            Vector2 normal;

            // ���� �̸��̳� ��ġ ���� ���� ����/���� ���� ����
            if (other.gameObject.name.Contains("VerticalWall")) // ���� �� (����, ������ ��)
            {
                normal = Vector2.right; // ���� ���⿡���� �ݻ� ó��
            }
            else if (other.gameObject.name.Contains("HorizontalWall")) // ���� �� (��, �Ʒ� ��)
            {
                normal = Vector2.up; // ���� ���⿡���� �ݻ� ó��
            }
            else
            {
                normal = other.transform.up; // �⺻������ ���� Up ���� ���
            }

            // �ݻ� ó��
            Vector2 reflectDir = Vector2.Reflect(direction, normal).normalized;
            direction = new Vector3(reflectDir.x, reflectDir.y, 0);
        }

        // �浹�� ������Ʈ�� �÷��̾��� ���
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    // ������ ����� �� ȣ��Ǵ� �޼���
    private void HandleBossDeath()
    {
        Destroy(gameObject); // ������ ����ϸ� ����ü�� ����
    }
}
