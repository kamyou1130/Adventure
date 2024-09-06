using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject arrowPrefab; // 기본 화살 프리팹
    public GameObject bigArrowPrefab; // 큰 화살 프리팹 (궁극기)
    public Transform firePoint; // 화살이 발사되는 위치
    public float arrowRate = 0.3f;

    public float multiArrowRate = 1f;
    public float bigArrowRate = 1f; // 큰 화살 발사 속도

    private float nextArrow;
    private float nextmultiArrow;
    private float nextBigArrow;

    public MultiArrow multiArrow; // MultiArrowShooter 참조

    private Animator animator;
    private Vector2 fireDirection = Vector2.right; // 화살 발사 방향
    private Rigidbody2D rb;

    private static string unlockedSkillSceneX = "2-1"; // X 스킬이 사용 가능한 최소 씬
    private static string unlockedSkillSceneC = "3-1"; // C 스킬이 사용 가능한 최소 씬

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // 씬 이름 가져오기
        string currentScene = SceneManager.GetActiveScene().name;

        // 현재 씬이 unlockedSkillSceneX보다 같거나 이후 씬인 경우 X 스킬 사용 가능
        if (IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            unlockedSkillSceneX = currentScene;
        }

        // 현재 씬이 unlockedSkillSceneC보다 같거나 이후 씬인 경우 C 스킬 사용 가능
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

        // 플레이어 이동 처리
        transform.Translate(movement * speed * Time.deltaTime);


        // 씬 이름 가져오기
        string currentScene = SceneManager.GetActiveScene().name;

        // 기본 화살 발사
        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextArrow)
        {
            nextArrow = Time.time + arrowRate;
            FireArrow(); // 단일 화살 발사
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("Attack", false);
        }

        // 다중 화살 발사 (씬 이름이 unlockedSkillSceneX 이후인 경우만 가능)
        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextmultiArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            nextmultiArrow = Time.time + multiArrowRate;
            FireArrows(); // 다중 화살 발사
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("Attack", false);
        }

        // 큰 화살 발사 (궁극기, 씬 이름이 unlockedSkillSceneC 이후인 경우만 가능)
        if (Input.GetKeyDown(KeyCode.C) && Time.time > nextBigArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneC))
        {
            nextBigArrow = Time.time + bigArrowRate;
            FireBigArrow(); // 큰 화살 발사
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
            multiArrow.FireArrows(fireDirection); // 현재 방향으로 다중 화살 발사
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

    // 씬이 unlockedSkillScene보다 같거나 이후인 경우 true 반환
    bool IsSceneUnlocked(string currentScene, string unlockedScene)
    {
        return string.Compare(currentScene, unlockedScene, System.StringComparison.Ordinal) >= 0;
    }

    // 트리거 충돌 처리
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
