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
    public int healAmount = 1; // ȸ���Ǵ� ü�·�

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

        // Rigidbody�� ��Ȱ��ȭ
        rb.simulated = false;

        // �÷��̾� ��Ʈ�� ��ũ��Ʈ ��Ȱ��ȭ
        GetComponent<PlayerController>().enabled = false; // PlayerController ��ũ��Ʈ ��Ȱ��ȭ
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount ���
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        // ü���� �ִ� ü���� �ʰ����� �ʵ��� ����
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthUI(); // ü�� �� ������Ʈ
        Debug.Log("Player Health: " + currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heal"))
        {
            Heal(healAmount); // ������ �縸ŭ ü�� ȸ��
            Destroy(other.gameObject); // ȸ�� ������Ʈ ����
        }
    }
}
