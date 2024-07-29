using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    private Vector3 spawnOffset; // �߻� ��ġ ������

    void Start()
    {
        // �ʱ� �������� �÷��̾��� ���� ��ġ�� arrowSpawn�� ��� ��ġ
        spawnOffset = transform.position - player.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(player.position.x - 0.7f, player.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(player.position.x + 0.7f, player.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(player.position.x, player.position.y + 0.7f, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(player.position.x, player.position.y - 0.7f, 0);
        }
    }
}
