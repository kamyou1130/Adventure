using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject healPrefab; // ȸ�� ������Ʈ ������
    public float dropChance = 0.3f; // ȸ�� ������Ʈ ���� Ȯ�� (30%)

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Monster Died!");
        DropHealItem(); // ȸ�� ������Ʈ ���� �õ�
        Destroy(gameObject); // ���� ������Ʈ ����
    }

    void DropHealItem()
    {
        // Ȯ�������� ȸ�� ������Ʈ ����
        if (Random.value <= dropChance)
        {
            Instantiate(healPrefab, transform.position, Quaternion.identity);
        }
    }
}
