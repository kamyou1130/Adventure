using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 20f;
    private Vector2 direction;

    // ȭ���� ������ ��ġ
    private Vector2 spawnPosition;
    // ȭ���� �̵��� �ִ� �Ÿ�
    public float maxDistance = 10f;
    public int damage = 2; // ȭ���� ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        SetRotation();

        // ȭ���� ������ ��ġ�� ����
        spawnPosition = transform.position;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void SetRotation()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {
        // ȭ���� �̵��� �Ÿ� ���
        float distanceTravelled = Vector2.Distance(transform.position, spawnPosition);

        // �ִ� �Ÿ� �ʰ� �� ȭ�� ����
        if (distanceTravelled > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterHealth monsterHealth = other.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
