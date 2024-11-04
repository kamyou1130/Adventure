using System.Collections;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    public GameObject healPrefab; // ȸ�� ������Ʈ ������
    public float dropChance = 0.3f; // ȸ�� ������Ʈ ���� Ȯ�� (30%)

    public AudioClip hitSound;
    private AudioSource audioSource;

    // Start���� �ʱ�ȭ�� health ���� �Ź� ���� ������ �� �ֵ��� ����
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetMonster();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    // ���Ͱ� �״� ����
    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Monster Died!");
        DropHealItem(); // ȸ�� ������Ʈ ���� �õ�
        Destroy(gameObject); // ���� ������Ʈ ����
    }

    // Ȯ�������� ȸ�� ������Ʈ ����
    void DropHealItem()
    {
        if (Random.value <= dropChance)
        {
            Instantiate(healPrefab, transform.position, Quaternion.identity);
        }
    }

    // ���͸� �ٽ� ������ �� �ִ� �Լ�
    public void ResetMonster()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
