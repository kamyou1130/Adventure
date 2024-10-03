using UnityEngine;
using UnityEngine.AI;

public class BossAI_2 : MonoBehaviour
{
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위
    public float attackRange = 2.0f; // 공격 범위
    public float fireRate = 5f; // 투사체 발사 주기
    public GameObject projectilePrefab; // 투사체 프리팹
    public Transform firePoint; // 투사체 발사 위치
    public float projectileSpeed = 5f; // 투사체 속도
    public int projectileCount = 12; // 360도 방향으로 발사할 투사체의 개수

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // 플레이어를 나타내는 GameObject
    private float nextFireTime = 0f; // 다음 투사체 발사 시간

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            if (distanceToPlayer <= detectionRange)
            {
                if (!animator.GetBool("Attack"))
                {
                    navMeshAgent.SetDestination(player.transform.position);
                    HandleMovementAnimation(directionToPlayer);
                }

                if (distanceToPlayer <= attackRange && Time.time >= nextFireTime)
                {
                    AttackPlayer();
                    nextFireTime = Time.time + fireRate;
                }
            }
            else
            {
                navMeshAgent.ResetPath();
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void HandleMovementAnimation(Vector3 directionToPlayer)
    {
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

        animator.SetBool("Idle", false);
    }

    void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("Attack", true);

        // 360도 전방향으로 투사체 발사
        FireProjectilesInAllDirections();

        // 1초 후 Idle 상태로 전환
        Invoke("ResetAttack", 1f);
    }

    void ResetAttack()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true);
        navMeshAgent.isStopped = false;
    }

    void FireProjectilesInAllDirections()
    {
        float angleStep = 360f / projectileCount;
        float currentAngle = 0f;

        for (int i = 0; i < projectileCount; i++)
        {
            float projectileDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector3 projectileDirection = new Vector3(projectileDirX, projectileDirY, 0).normalized;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            float angle = Mathf.Atan2(projectileDirY, projectileDirX) * Mathf.Rad2Deg - 90f;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = projectileDirection * projectileSpeed;
            }

            currentAngle += angleStep;
        }
    }
}
