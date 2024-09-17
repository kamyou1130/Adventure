using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1; // 플레이어에게 줄 데미지
    public float maxDistance = 5.0f; // 투사체가 이동할 최대 거리

    private Vector3 startPosition; // 투사체가 발사된 시작 위치

    void Start()
    {
        // 투사체의 시작 위치를 기록합니다.
        startPosition = transform.position;
    }

    void Update()
    {
        // 현재 위치와 시작 위치 사이의 거리를 계산합니다.
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);

        // 거리가 최대 거리를 넘어가면 투사체를 파괴합니다.
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 Player 태그를 가지고 있는지 확인
        if (collision.CompareTag("Player"))
        {
            // Player에게 데미지를 주는 함수 호출 (플레이어가 데미지 처리 함수를 가지고 있다고 가정)
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // 플레이어에게 데미지 주기
            }

            // 충돌 후 투사체 제거
            Destroy(gameObject);
        }
    }
}
