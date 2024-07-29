using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Boundary boundary;
    public GameObject arrowPrefab;
    public Transform firePoint; // 화살이 발사되는 위치
    public float arrowRate = 0.25f;
    private float nextArrow;

    private Animator animator;
    private Vector2 fireDirection = Vector2.right; // 화살 발사 방향
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // 플레이어의 움직임을 직접 제어하기 위해 Kinematic으로 설정
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement = new Vector3(-1, 0, 0);
            animator.SetBool("Left", true);
            fireDirection = Vector2.left;
        }
        else
        {
            animator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement = new Vector3(1, 0, 0);
            animator.SetBool("Right", true);
            fireDirection = Vector2.right;
        }
        else
        {
            animator.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement = new Vector3(0, 1, 0);
            animator.SetBool("Up", true);
            fireDirection = Vector2.up;
        }
        else
        {
            animator.SetBool("Up", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement = new Vector3(0, -1, 0);
            animator.SetBool("Down", true);
            fireDirection = Vector2.down;
        }
        else
        {
            animator.SetBool("Down", false);
        }

        if (movement == Vector3.zero)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }

        // 플레이어 이동 처리
        transform.Translate(movement * speed * Time.deltaTime);

        // 맵 경계 내에 플레이어 위치 제한
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            transform.position.z
        );

        transform.position = clampedPosition;

        // 화살 발사
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextArrow)
        {
            nextArrow = Time.time + arrowRate;
            FireArrow();
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    void FireArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        ArrowMover arrowMover = arrow.GetComponent<ArrowMover>();
        if (arrowMover != null)
        {
            arrowMover.SetDirection(fireDirection);
        }
    }

    // 트리거 충돌 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        // 벽과의 충돌 처리
        if (other.CompareTag("Wall"))
        {
            // 충돌 시 이동 불가 처리, 필요한 경우 다른 논리를 추가할 수 있음
            rb.velocity = Vector2.zero;

            // 추가적인 로직으로, 플레이어를 충돌 지점에서 밀어내는 방식도 고려할 수 있습니다.
        }
    }
}
