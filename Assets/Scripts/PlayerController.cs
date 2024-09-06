using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject arrowPrefab; // �⺻ ȭ�� ������
    public GameObject bigArrowPrefab; // ū ȭ�� ������ (�ñر�)
    public Transform firePoint; // ȭ���� �߻�Ǵ� ��ġ
    public float arrowRate = 0.3f;

    public float multiArrowRate = 1f;
    public float bigArrowRate = 1f; // ū ȭ�� �߻� �ӵ�

    private float nextArrow;
    private float nextmultiArrow;
    private float nextBigArrow;

    public MultiArrow multiArrow; // MultiArrowShooter ����

    private Animator animator;
    private Vector2 fireDirection = Vector2.right; // ȭ�� �߻� ����
    private Rigidbody2D rb;

    private static string unlockedSkillSceneX = "2-1"; // X ��ų�� ��� ������ �ּ� ��
    private static string unlockedSkillSceneC = "3-1"; // C ��ų�� ��� ������ �ּ� ��

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // �� �̸� ��������
        string currentScene = SceneManager.GetActiveScene().name;

        // ���� ���� unlockedSkillSceneX���� ���ų� ���� ���� ��� X ��ų ��� ����
        if (IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            unlockedSkillSceneX = currentScene;
        }

        // ���� ���� unlockedSkillSceneC���� ���ų� ���� ���� ��� C ��ų ��� ����
        if (IsSceneUnlocked(currentScene, unlockedSkillSceneC))
        {
            unlockedSkillSceneC = currentScene;
        }
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


        // �� �̸� ��������
        string currentScene = SceneManager.GetActiveScene().name;

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

        // ���� ȭ�� �߻� (�� �̸��� unlockedSkillSceneX ������ ��츸 ����)
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextmultiArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            nextmultiArrow = Time.time + multiArrowRate;
            FireArrows(); // ���� ȭ�� �߻�
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("Attack", false);
        }

        // ū ȭ�� �߻� (�ñر�, �� �̸��� unlockedSkillSceneC ������ ��츸 ����)
        if (Input.GetKeyDown(KeyCode.C) && Time.time > nextBigArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneC))
        {
            nextBigArrow = Time.time + bigArrowRate;
            FireBigArrow(); // ū ȭ�� �߻�
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.C))
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

    void FireBigArrow()
    {
        GameObject bigArrow = Instantiate(bigArrowPrefab, firePoint.position, Quaternion.identity);
        ArrowMover arrowMover = bigArrow.GetComponent<ArrowMover>();
        if (arrowMover != null)
        {
            arrowMover.SetDirection(fireDirection);
        }
    }

    // ���� unlockedSkillScene���� ���ų� ������ ��� true ��ȯ
    bool IsSceneUnlocked(string currentScene, string unlockedScene)
    {
        return string.Compare(currentScene, unlockedScene, System.StringComparison.Ordinal) >= 0;
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
