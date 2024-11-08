using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;        // Panel 오브젝트
    public float moveSpeed = 50f;   // 패널이 올라가는 속도
    public float delayTime = 5f;    // 패널이 사라지기 전 대기 시간

    private Vector3 initialPosition;
    private bool shouldMove = false;

    void Start()
    {
        initialPosition = panel.transform.position;
        Invoke("StartMovingPanel", delayTime);
    }

    void StartMovingPanel()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove)
        {
            panel.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // 일정 높이에 도달하면 패널을 비활성화하고 이동을 중단
            if (panel.transform.position.y >= initialPosition.y + 200) // 원하는 높이만큼 설정
            {
                panel.SetActive(false);
                shouldMove = false;
            }
        }
    }
}
