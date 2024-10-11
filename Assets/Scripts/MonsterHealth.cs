using System.Collections;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    public GameObject healPrefab; // 회복 오브젝트 프리팹
    public float dropChance = 0.3f; // 회복 오브젝트 생성 확률 (30%)

    // Start에서 초기화된 health 값을 매번 새로 세팅할 수 있도록 수정
    void Start()
    {
        ResetMonster();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    // 몬스터가 죽는 로직
    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Monster Died!");
        DropHealItem(); // 회복 오브젝트 생성 시도
        Destroy(gameObject); // 몬스터 오브젝트 삭제
    }

    // 확률적으로 회복 오브젝트 생성
    void DropHealItem()
    {
        if (Random.value <= dropChance)
        {
            Instantiate(healPrefab, transform.position, Quaternion.identity);
        }
    }

    // 몬스터를 다시 리셋할 수 있는 함수
    public void ResetMonster()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
