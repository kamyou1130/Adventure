using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public Vector2 offset;    // ī�޶�� �÷��̾� ������ �⺻ �Ÿ�
    public Vector2 boundarySize = new Vector2(5f, 3f); // ī�޶� �������� �ʴ� ������ ũ��
    public float lerpSpeed = 5f; // ī�޶� ���󰡴� �ӵ� (Lerp �ӵ�)

    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        // ī�޶� �ٿ������ �ʱⰪ ����
        Vector3 playerPos = player.position;
        minBounds = playerPos - new Vector3(boundarySize.x, boundarySize.y, 0);
        maxBounds = playerPos + new Vector3(boundarySize.x, boundarySize.y, 0);
    }

    void LateUpdate()
    {
        // �÷��̾� ��ġ
        Vector3 playerPos = player.position;
        Vector3 cameraPos = transform.position;

        // �÷��̾ �ٿ���� ������ ������ ī�޶� �̵�
        if (playerPos.x < minBounds.x || playerPos.x > maxBounds.x)
        {
            cameraPos.x = playerPos.x + offset.x;
            UpdateBounds();
        }

        if (playerPos.y < minBounds.y || playerPos.y > maxBounds.y)
        {
            cameraPos.y = playerPos.y + offset.y;
            UpdateBounds();
        }

        // �ε巯�� �̵��� ���� Lerp ���
        transform.position = Vector3.Lerp(transform.position, new Vector3(cameraPos.x, cameraPos.y, cameraPos.z), lerpSpeed * Time.deltaTime);
    }

    void UpdateBounds()
    {
        // �ٿ������ ������Ʈ
        minBounds = new Vector3(transform.position.x - boundarySize.x, transform.position.y - boundarySize.y, 0);
        maxBounds = new Vector3(transform.position.x + boundarySize.x, transform.position.y + boundarySize.y, 0);
    }
}
