using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject arrowPrefab;
    public GameObject bigArrowPrefab;
    public Transform firePoint;
    public float arrowRate = 0.3f;

    public float multiArrowRate = 1f;
    public float bigArrowRate = 1f;

    private float nextArrow;
    private float nextmultiArrow;
    private float nextBigArrow;

    public MultiArrow multiArrow;

    public AudioClip arrowSound;
    public AudioClip fireArrowSound;
    private AudioSource audioSource;

    private Animator animator;
    private Vector2 fireDirection = Vector2.right;
    private Rigidbody2D rb;

    private static string unlockedSkillSceneX = "2-1";
    private static string unlockedSkillSceneC = "3-1";

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        string currentScene = SceneManager.GetActiveScene().name;

        if (IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            unlockedSkillSceneX = currentScene;
        }

        if (IsSceneUnlocked(currentScene, unlockedSkillSceneC))
        {
            unlockedSkillSceneC = currentScene;
        }
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
        {
            movement = new Vector3(-1, 0, 0);
            animator.SetBool("Left", true);
            fireDirection = Vector2.left;
        }
        else
        {
            animator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
        {
            movement = new Vector3(1, 0, 0);
            animator.SetBool("Right", true);
            fireDirection = Vector2.right;
        }
        else
        {
            animator.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
        {
            movement = new Vector3(0, 1, 0);
            animator.SetBool("Up", true);
            fireDirection = Vector2.up;
        }
        else
        {
            animator.SetBool("Up", false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
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

        transform.Translate(movement * speed * Time.deltaTime);

        string currentScene = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.Z) && Time.time > nextArrow)
        {
            nextArrow = Time.time + arrowRate;
            FireArrow();
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            animator.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextmultiArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneX))
        {
            nextmultiArrow = Time.time + multiArrowRate;
            FireArrows();
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("Attack", false);
        }

        if (Input.GetKeyDown(KeyCode.C) && Time.time > nextBigArrow && IsSceneUnlocked(currentScene, unlockedSkillSceneC))
        {
            nextBigArrow = Time.time + bigArrowRate;
            FireBigArrow();
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

        if (arrowSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(arrowSound);
        }
    }

    void FireArrows()
    {
        if (multiArrow != null)
        {
            multiArrow.FireArrows(fireDirection);
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
        if (fireArrowSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireArrowSound);
        }
    }

    bool IsSceneUnlocked(string currentScene, string unlockedScene)
    {
        return string.Compare(currentScene, unlockedScene, System.StringComparison.Ordinal) >= 0;
    }

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
