using UnityEngine;
using UnityEngine.AI;

public class BossAI_4 : MonoBehaviour
{
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����
    public float attackRange = 2.0f; // ���� ����
    public float fireRate = 5f; // ����ü �߻� �ֱ�
    public GameObject projectilePrefab; // ����ü ������
    public Transform firePoint; // ����ü �߻� ��ġ
    public float projectileSpeed = 5f; // ����ü �ӵ�

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // �÷��̾ ��Ÿ���� GameObject
    private float nextFireTime = 0f; // ���� ����ü �߻� �ð�

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        animator = GetComponent<Animator>();

        // "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // �÷��̾� ��� ��ġ ���
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // ���� ���� ���� �ִ� ��� ����
            if (distanceToPlayer <= detectionRange)
            {
                // ���� ���� �ƴ� ���� �÷��̾ ����
                if (!animator.GetBool("Attack"))
                {
                    navMeshAgent.SetDestination(player.transform.position);
                    HandleMovementAnimation(directionToPlayer);
                }

                // ���� ���� ���� �ִ� ��� ����
                if (distanceToPlayer <= attackRange)
                {
                    // 5�ʸ��� �� ���� ����
                    if (Time.time >= nextFireTime)
                    {
                        AttackPlayer(directionToPlayer);
                        nextFireTime = Time.time + fireRate;
                    }
                }
            }
            else
            {
                // ���� ���� �ۿ� �÷��̾ �ִ� ��� ����
                navMeshAgent.ResetPath();
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            // �÷��̾ ������ Idle ���� ����, �ٽ� ã�� �õ�
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void HandleMovementAnimation(Vector3 directionToPlayer)
    {
        // ������ �÷��̾�� ���ʿ� ������ ������ ����, �����ʿ� ������ �������� ���� ����
        if (directionToPlayer.x < 0)
        {
            animator.SetBool("Left", true);
            animator.SetBool("Right", false);
        }
        else
        {
            animator.SetBool("Right", true);
            animator.SetBool("Left", false);
        }

        animator.SetBool("Idle", false);  // Idle ���¸� ����
    }

    void AttackPlayer(Vector3 directionToPlayer)
    {
        // ���� �߿��� NavMeshAgent ���߱�
        navMeshAgent.isStopped = true;

        // �÷��̾� ���⿡ ���� ���� �ִϸ��̼� ���
        if (directionToPlayer.x < 0)
        {
            animator.SetBool("Left", false);
            animator.SetBool("Attack", true);  // ���� ����
        }
        else
        {
            animator.SetBool("Right", false);
            animator.SetBool("Attack", true);  // ������ ����
        }

        // ���� �� ���� �ð��� ������ �ٽ� Idle ���·� ��ȯ �� �̵� �簳
        Invoke("ResetAttack", 1f);  // ���� �ִϸ��̼��� ���� �� 1�� �ڿ� Idle�� ���ư�

        // ����ü �߻�
        FireProjectile(directionToPlayer);
    }

    void ResetAttack()
    {
        // ���� �ִϸ��̼��� ������ Attack�� false�� ����
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true); // Idle ���·� ��ȯ

        // ������ ������ �ٽ� NavMeshAgent Ȱ��ȭ �� �̵� �簳
        navMeshAgent.isStopped = false;
    }

    void FireProjectile(Vector3 directionToPlayer)
    {
        // ����ü ����
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // ����ü �̵� �� ƨ�� ó�� ��ũ��Ʈ ����
        Projectile_boss4 projectileScript = projectile.GetComponent<Projectile_boss4>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(directionToPlayer, projectileSpeed); // LayerMask ����
        }
    }
}
