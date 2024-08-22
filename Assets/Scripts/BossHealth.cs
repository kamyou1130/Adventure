using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20; // 보스의 최대 체력
    private int currentHealth;

    public Image fillImage; // 보스 체력 바에 사용할 채워지는 이미지
    void Start()
    {
        if (fillImage != null)
        {
            currentHealth = maxHealth;

            // 초기 체력 바 설정
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

        if (currentHealth <= 0)
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
        // 보스 사망 처리
        Debug.Log("Boss Died!");

        // UI도 함께 삭제하거나 비활성화
        if (fillImage != null)
        {
            fillImage.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }


    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount 사용
    }
}
