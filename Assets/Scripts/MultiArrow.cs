using UnityEngine;

public class MultiArrow : MonoBehaviour
{
    public GameObject arrowPrefab; // 화살 프리팹
    public Transform firePoint;    // 화살이 발사될 위치
    public int arrowCount = 5;     // 발사할 화살의 수
    public float spreadAngle = 30f; // 화살이 퍼지는 각도
    public float arrowSpeed = 20f; // 화살의 속도
    public float maxDistance = 10f; // 화살의 최대 거리

    private Vector2 fireDirection;

    public void FireArrows(Vector2 direction)
    {
        fireDirection = direction;
        float angleStep = spreadAngle / (arrowCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < arrowCount; i++)
        {
            // 각 화살의 각도 계산
            float angle = startAngle + (angleStep * i);
            Vector2 arrowDirection = Quaternion.Euler(0, 0, angle) * fireDirection;

            // 화살 프리팹 인스턴스화 및 방향 설정
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
            ArrowMover arrowMover = arrow.GetComponent<ArrowMover>();
            if (arrowMover != null)
            {
                arrowMover.SetDirection(arrowDirection);
                arrowMover.speed = arrowSpeed;
                arrowMover.maxDistance = maxDistance;
            }
        }
    }
}
