using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax; // �̵� ���� ���� x,y��ǥ�� �ִ밪�� �ּҰ�
}

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // �̵��ӵ��� ���� ����
    public Boundary boundary; // �̵� ���� ������ ���� ����

    private Animator animator; // �ִϸ����� ������Ʈ�� ���� ����

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator ������Ʈ�� �����ɴϴ�.
    }

    void Update()
    {
        // �̵� ���͸� �ʱ�ȭ
        Vector3 movement = Vector3.zero;

        // Ű���� �Է��� �޾� �̵� ���͸� �����ϰ� �ִϸ��̼� Ʈ���� ����
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement = new Vector3(-1, 0, 0);
            animator.SetBool("Left", true);
        }
        else
        {
            animator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement = new Vector3(1, 0, 0);
            animator.SetBool("Right", true);
        }
        else
        {
            animator.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movement = new Vector3(0, 1, 0);
            animator.SetBool("Up", true);
        }
        else
        {
            animator.SetBool("Up", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movement = new Vector3(0, -1, 0);
            animator.SetBool("Down", true);
        }
        else
        {
            animator.SetBool("Down", false);
        }

        // ��� ���� �Է��� ���� �� Idle ���·� ��ȯ
        if (movement == Vector3.zero)
        {
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Idle", false);
        }

        // �÷��̾��� ���� ��ġ�� �̵� ���͸� ����
        transform.Translate(movement * speed * Time.deltaTime);

        // �÷��̾��� ��ġ�� �̵� ���� ���� ���� ���� ����
        Vector3 clampedPosition = new Vector3
        (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax),
            transform.position.z
        );

        transform.position = clampedPosition;
    }
}
