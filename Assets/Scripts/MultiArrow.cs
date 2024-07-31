using UnityEngine;

public class MultiArrow : MonoBehaviour
{
    public GameObject arrowPrefab; // ȭ�� ������
    public Transform firePoint;    // ȭ���� �߻�� ��ġ
    public int arrowCount = 5;     // �߻��� ȭ���� ��
    public float spreadAngle = 30f; // ȭ���� ������ ����
    public float arrowSpeed = 20f; // ȭ���� �ӵ�
    public float maxDistance = 10f; // ȭ���� �ִ� �Ÿ�

    private Vector2 fireDirection;

    public void FireArrows(Vector2 direction)
    {
        fireDirection = direction;
        float angleStep = spreadAngle / (arrowCount - 1);
        float startAngle = -spreadAngle / 2;

        for (int i = 0; i < arrowCount; i++)
        {
            // �� ȭ���� ���� ���
            float angle = startAngle + (angleStep * i);
            Vector2 arrowDirection = Quaternion.Euler(0, 0, angle) * fireDirection;

            // ȭ�� ������ �ν��Ͻ�ȭ �� ���� ����
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
