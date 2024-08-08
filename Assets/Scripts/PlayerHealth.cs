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

    public Image fillImage; // ä������ �̹���

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // �ʱ� ü�� �� ����
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI(); // ü�� �� ������Ʈ

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
        // �÷��̾� ��� ó��
        animator.SetBool("Die", true);
        Debug.Log("Player Died!");
        // ���⿡ ���� ���� ó�� ���� �߰�
        rb.simulated = false;
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount ���
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI(); // ü�� �� ������Ʈ
        Debug.Log("Player Health: " + currentHealth);
    }
}
