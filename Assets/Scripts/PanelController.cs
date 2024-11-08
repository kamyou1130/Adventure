using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;        // Panel ������Ʈ
    public float moveSpeed = 50f;   // �г��� �ö󰡴� �ӵ�
    public float delayTime = 5f;    // �г��� ������� �� ��� �ð�

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

            // ���� ���̿� �����ϸ� �г��� ��Ȱ��ȭ�ϰ� �̵��� �ߴ�
            if (panel.transform.position.y >= initialPosition.y + 200) // ���ϴ� ���̸�ŭ ����
            {
                panel.SetActive(false);
                shouldMove = false;
            }
        }
    }
}
