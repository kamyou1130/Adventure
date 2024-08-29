using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth; // 체력 상태
    private Animator animator;
    private Rigidbody2D rb;

    public Image fillImage; // 체력 바에 사용할 채워지는 이미지
    public int healAmount = 1; // 회복되는 체력량

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // 체력 초기화
        currentHealth = maxHealth;

        // 체력 바 UI 업데이트
        UpdateHealthUI();

        // 이전 씬에서 저장된 체력 정보 로드
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

    // 체력 저장
    public void SaveHealth()
    {
        PlayerData.Instance.SavePlayerHealth(currentHealth);
    }

    // 체력 로드
    public void LoadHealth()
    {
        currentHealth = PlayerData.Instance.LoadPlayerHealth();
        UpdateHealthUI();
    }
}
