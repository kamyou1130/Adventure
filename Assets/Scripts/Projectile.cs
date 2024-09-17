using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // �÷��̾�� �� ������
    public float maxDistance = 5.0f; // ����ü�� �̵��� �ִ� �Ÿ�

    private Vector3 startPosition; // ����ü�� �߻�� ���� ��ġ

    void Start()
    {
        // ����ü�� ���� ��ġ�� ����մϴ�.
        startPosition = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� ���� ��ġ ������ �Ÿ��� ����մϴ�.
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        // �Ÿ��� �ִ� �Ÿ��� �Ѿ�� ����ü�� �ı��մϴ�.
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� Player �±׸� ������ �ִ��� Ȯ��
        if (collision.CompareTag("Player"))
        {
            // Player���� �������� �ִ� �Լ� ȣ�� (�÷��̾ ������ ó�� �Լ��� ������ �ִٰ� ����)
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // �÷��̾�� ������ �ֱ�
            }

            // �浹 �� ����ü ����
            Destroy(gameObject);
        }
    }
}
