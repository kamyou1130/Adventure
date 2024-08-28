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
    public int healAmount = 1; // 회복되는 체력량

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

        // Rigidbody를 비활성화
        rb.simulated = false;

        // 플레이어 컨트롤 스크립트 비활성화
        GetComponent<PlayerController>().enabled = false; // PlayerController 스크립트 비활성화
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount 사용
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        // 체력이 최대 체력을 초과하지 않도록 제한
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI(); // 체력 바 업데이트
        Debug.Log("Player Health: " + currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heal"))
        {
            Heal(healAmount); // 지정된 양만큼 체력 회복
            Destroy(other.gameObject); // 회복 오브젝트 제거
        }
    }
}
