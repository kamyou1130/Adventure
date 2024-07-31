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
    public Transform firePoint; // ȭ���� �߻�Ǵ� ��ġ
    public float arrowRate = 0.25f;
    private float nextArrow;

    public MultiArrow multiArrow; // MultiArrowShooter ����

    private Animator animator;
    private Vector2 fireDirection = Vector2.right; // ȭ�� �߻� ����
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

        // �÷��̾� �̵� ó��
        transform.Translate(movement * speed * Time.deltaTime);

        // �� ��� ���� �÷��̾� ��ġ ����
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            transform.position.z
        );

        transform.position = clampedPosition;

        // �⺻ ȭ�� �߻�
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextArrow)
        {
            nextArrow = Time.time + arrowRate;
            FireArrow(); // ���� ȭ�� �߻�
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("Attack", false);
        }

        // ���� ȭ�� �߻�
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextArrow)
        {
            nextArrow = Time.time + arrowRate;
            FireArrows(); // ���� ȭ�� �߻�
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp (KeyCode.X))
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

    void FireArrows()
    {
        if (multiArrow != null)
        {
            multiArrow.FireArrows(fireDirection); // ���� �������� ���� ȭ�� �߻�
        }
    }

    // Ʈ���� �浹 ó��
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            Vector2 pushBack = collision.contacts[0].normal * 0.1f;
            rb.position += pushBack;
        }
    }
}
