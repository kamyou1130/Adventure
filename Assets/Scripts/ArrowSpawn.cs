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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
        {
            transform.position = new Vector3(player.position.x - 0.7f, player.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
        {
            transform.position = new Vector3(player.position.x + 0.7f, player.position.y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
        {
            transform.position = new Vector3(player.position.x, player.position.y + 0.7f, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
        {
            transform.position = new Vector3(player.position.x, player.position.y - 0.7f, 0);
        }
    }
}
