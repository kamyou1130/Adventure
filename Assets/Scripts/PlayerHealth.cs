using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rb;

    public Image fillImage; // 채워지는 이미지

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // 초기 체력 바 설정
        UpdateHealthUI();
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
            Debug.Log("Player Health: " + currentHealth);
        }
    }

    void Die()
    {
        // 플레이어 사망 처리
        animator.SetBool("Die", true);
        Debug.Log("Player Died!");
        // 여기에 게임 오버 처리 등을 추가
        rb.simulated = false;
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount 사용
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI(); // 체력 바 업데이트
        Debug.Log("Player Health: " + currentHealth);
    }
}
