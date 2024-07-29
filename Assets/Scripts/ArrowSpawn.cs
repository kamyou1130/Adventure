using UnityEngine;

public class ArrowSpawn : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    private Vector3 spawnOffset; // 발사 위치 오프셋

    void Start()
    {
        // 초기 오프셋은 플레이어의 현재 위치와 arrowSpawn의 상대 위치
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
