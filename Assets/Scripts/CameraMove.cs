using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public Vector2 offset;    // 카메라와 플레이어 사이의 기본 거리
    public Vector2 boundarySize = new Vector2(5f, 3f); // 카메라가 움직이지 않는 범위의 크기
    public float lerpSpeed = 5f; // 카메라가 따라가는 속도 (Lerp 속도)

    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        // 카메라 바운더리의 초기값 설정
        Vector3 playerPos = player.position;
        minBounds = playerPos - new Vector3(boundarySize.x, boundarySize.y, 0);
        maxBounds = playerPos + new Vector3(boundarySize.x, boundarySize.y, 0);
    }

    void LateUpdate()
    {
        // 플레이어 위치
        Vector3 playerPos = player.position;
        Vector3 cameraPos = transform.position;

        // 플레이어가 바운더리 밖으로 나가면 카메라 이동
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

        // 부드러운 이동을 위해 Lerp 사용
        transform.position = Vector3.Lerp(transform.position, new Vector3(cameraPos.x, cameraPos.y, cameraPos.z), lerpSpeed * Time.deltaTime);
    }

    void UpdateBounds()
    {
        // 바운더리를 업데이트
        minBounds = new Vector3(transform.position.x - boundarySize.x, transform.position.y - boundarySize.y, 0);
        maxBounds = new Vector3(transform.position.x + boundarySize.x, transform.position.y + boundarySize.y, 0);
    }
}
