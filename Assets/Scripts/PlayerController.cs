using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax; // 이동 가능 영역 x,y좌표의 최대값과 최소값
}

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // 이동속도를 위한 변수
    public Boundary boundary; // 이동 가능 영역을 위한 변수
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator 컴포넌트를 가져옴
    }

    void Update()
    {
        // 이동 벡터를 초기화
        Vector3 movement = Vector3.zero;

        // 키보드 입력을 받아 이동 벡터를 생성
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement = new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement = new Vector3(1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement = new Vector3(0, 1, 0);
            animator.SetTrigger("Up");
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement = new Vector3(0, -1, 0);
            animator.SetTrigger("Down");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        // 플레이어의 현재 위치에 이동 벡터를 적용
        transform.Translate(movement * speed * Time.deltaTime);

        // 플레이어의 위치를 이동 가능 영역 범위 내로 제한
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            transform.position.z
        );

        transform.position = clampedPosition;
    }
}
