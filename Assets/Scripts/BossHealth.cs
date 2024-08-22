using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20; // ������ �ִ� ü��
    private int currentHealth;

    public Image fillImage; // ���� ü�� �ٿ� ����� ä������ �̹���
    void Start()
    {
        if (fillImage != null)
        {
            currentHealth = maxHealth;

            // �ʱ� ü�� �� ����
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

        UpdateHealthUI(); // ü�� �� ������Ʈ

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
        // ���� ��� ó��
        Debug.Log("Boss Died!");

        // UI�� �Բ� �����ϰų� ��Ȱ��ȭ
        if (fillImage != null)
        {
            fillImage.gameObject.SetActive(false);
        }

        Destroy(gameObject);
    }


    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount ���
    }
}
