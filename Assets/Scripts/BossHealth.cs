using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20; // 보스의 최대 체력
    public int currentHealth;
    private bool isDead = false;

    public GameObject healPrefab; // 회복 오브젝트 프리팹
    public Image fillImage; // 보스 체력 바에 사용할 채워지는 이미지

    // 보스 사망 이벤트 정의
    public event Action OnBossDeath;

    void Start()
    {
        if (fillImage != null)
        {
            currentHealth = maxHealth;
            UpdateHealthUI();
        }
        else
        {
            Debug.LogError("Fill Image is not assigned in the Inspector!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI(); // 체력 바 업데이트

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            Debug.Log("Boss Health: " + currentHealth);
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Boss Died!");
        DropHealItem();

        // UI도 함께 삭제하거나 비활성화
        if (fillImage != null)
        {
            fillImage.gameObject.SetActive(false);
        }

        // 사망 이벤트 발생
        OnBossDeath?.Invoke();

        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount 사용
    }

    void DropHealItem()
    {
        Instantiate(healPrefab, transform.position, Quaternion.identity);
    }
}
