using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 20f;
    private Vector2 direction;

    // 화살이 생성된 위치
    private Vector2 spawnPosition;
    // 화살이 이동할 최대 거리
    public float maxDistance = 10f;
    public int damage = 2; // 화살의 데미지

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
        SetRotation();

        // 화살이 생성된 위치를 저장
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
        // 화살이 이동한 거리 계산
        float distanceTravelled = Vector2.Distance(transform.position, spawnPosition);

        // 최대 거리 초과 시 화살 제거
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
