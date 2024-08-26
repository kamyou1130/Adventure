using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject healPrefab; // 회복 오브젝트 프리팹
    public float dropChance = 0.3f; // 회복 오브젝트 생성 확률 (30%)

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
        DropHealItem(); // 회복 오브젝트 생성 시도
        Destroy(gameObject); // 몬스터 오브젝트 삭제
    }

    void DropHealItem()
    {
        // 확률적으로 회복 오브젝트 생성
        if (Random.value <= dropChance)
        {
            Instantiate(healPrefab, transform.position, Quaternion.identity);
        }
    }
}
