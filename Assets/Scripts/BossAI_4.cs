using UnityEngine;
using UnityEngine.AI;

public class BossAI_4 : MonoBehaviour
{
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위
    public float attackRange = 2.0f; // 공격 범위
    public float fireRate = 5f; // 투사체 발사 주기
    public GameObject projectilePrefab; // 투사체 프리팹
    public Transform firePoint; // 투사체 발사 위치
    public float projectileSpeed = 5f; // 투사체 속도

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

        // "Player" 태그를 가진 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // 플레이어 상대 위치 계산
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // 추적 범위 내에 있는 경우 추적
            if (distanceToPlayer <= detectionRange)
            {
                // 공격 중이 아닐 때만 플레이어를 추적
                if (!animator.GetBool("Attack"))
                {
                    navMeshAgent.SetDestination(player.transform.position);
                    HandleMovementAnimation(directionToPlayer);
                }

                // 공격 범위 내에 있는 경우 공격
                if (distanceToPlayer <= attackRange)
                {
                    // 5초마다 한 번씩 공격
                    if (Time.time >= nextFireTime)
                    {
                        AttackPlayer(directionToPlayer);
                        nextFireTime = Time.time + fireRate;
                    }
                }
            }
            else
            {
                // 추적 범위 밖에 플레이어가 있는 경우 멈춤
                navMeshAgent.ResetPath();
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            // 플레이어가 없으면 Idle 상태 유지, 다시 찾기 시도
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void HandleMovementAnimation(Vector3 directionToPlayer)
    {
        // 보스가 플레이어보다 왼쪽에 있으면 왼쪽을 보고, 오른쪽에 있으면 오른쪽을 보게 설정
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

        animator.SetBool("Idle", false);  // Idle 상태를 해제
    }

    void AttackPlayer(Vector3 directionToPlayer)
    {
        // 공격 중에는 NavMeshAgent 멈추기
        navMeshAgent.isStopped = true;

        // 플레이어 방향에 따른 공격 애니메이션 재생
        if (directionToPlayer.x < 0)
        {
            animator.SetBool("Left", false);
            animator.SetBool("Attack", true);  // 왼쪽 공격
        }
        else
        {
            animator.SetBool("Right", false);
            animator.SetBool("Attack", true);  // 오른쪽 공격
        }

        // 공격 후 일정 시간이 지나면 다시 Idle 상태로 전환 및 이동 재개
        Invoke("ResetAttack", 1f);  // 공격 애니메이션이 끝난 후 1초 뒤에 Idle로 돌아감

        // 투사체 발사
        FireProjectile(directionToPlayer);
    }

    void ResetAttack()
    {
        // 공격 애니메이션이 끝나면 Attack을 false로 설정
        animator.SetBool("Attack", false);
        animator.SetBool("Idle", true); // Idle 상태로 전환

        // 공격이 끝나면 다시 NavMeshAgent 활성화 및 이동 재개
        navMeshAgent.isStopped = false;
    }

    void FireProjectile(Vector3 directionToPlayer)
    {
        // 투사체 생성
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // 투사체 이동 및 튕김 처리 스크립트 설정
        Projectile_boss4 projectileScript = projectile.GetComponent<Projectile_boss4>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(directionToPlayer, projectileSpeed); // LayerMask 삭제
        }
    }
}
