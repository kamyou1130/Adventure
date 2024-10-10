using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 20; // ������ �ִ� ü��
    public int currentHealth;
    private bool isDead = false;

    public GameObject healPrefab; // ȸ�� ������Ʈ ������
    public Image fillImage; // ���� ü�� �ٿ� ����� ä������ �̹���

    // ���� ��� �̺�Ʈ ����
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

        UpdateHealthUI(); // ü�� �� ������Ʈ

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

        // UI�� �Բ� �����ϰų� ��Ȱ��ȭ
        if (fillImage != null)
        {
            fillImage.gameObject.SetActive(false);
        }

        // ��� �̺�Ʈ �߻�
        OnBossDeath?.Invoke();

        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent; // fillAmount ���
    }

    void DropHealItem()
    {
        Instantiate(healPrefab, transform.position, Quaternion.identity);
    }
}
