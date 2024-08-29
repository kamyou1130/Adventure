using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth; // ü�� ����
    private Animator animator;
    private Rigidbody2D rb;

    public Image fillImage; // ü�� �ٿ� ����� ä������ �̹���
    public int healAmount = 1; // ȸ���Ǵ� ü�·�

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // ü�� �ʱ�ȭ
        currentHealth = maxHealth;

        // ü�� �� UI ������Ʈ
        UpdateHealthUI();

        // ���� ������ ����� ü�� ���� �ε�
        LoadHealth();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("Die", true);
        rb.simulated = false;
        GetComponent<PlayerController>().enabled = false;
    }

    void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        fillImage.fillAmount = healthPercent;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heal"))
        {
            Heal(healAmount);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("BossHeal"))
        {
            Heal(10);
            Destroy(other.gameObject);
        }
    }

    // ü�� ����
    public void SaveHealth()
    {
        PlayerData.Instance.SavePlayerHealth(currentHealth);
    }

    // ü�� �ε�
    public void LoadHealth()
    {
        currentHealth = PlayerData.Instance.LoadPlayerHealth();
        UpdateHealthUI();
    }
}
