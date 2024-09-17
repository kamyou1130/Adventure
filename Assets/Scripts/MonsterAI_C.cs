using UnityEngine;
using UnityEngine.AI;
using System.Collections;  // IEnumerator를 사용하기 위한 지시문

public class MonsterAI_C : MonoBehaviour
{
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위
    public float attackRange = 3.0f;  // 공격 애니메이션을 재생할 거리
    public GameObject projectilePrefab;  // 투사체 프리팹
    public Transform firePoint;  // 투사체가 발사될 위치
    public float fireRate = 1.0f;  // 투사체 발사 간격 (초 단위)

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform player;  // 플레이어의 Transform을 저장할 변수
    private bool isAttacking = false;  // 공격 상태 플래그
    private float nextFireTime = 0f;  // 다음 발사 가능 시간

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();

        // Player 태그를 가진 오브젝트를 찾아서 그 Transform을 저장합니다.
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 플레이어 상대 위치 계산
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // 몬스터의 로컬 좌표계에서 플레이어의 방향 계산
            Vector3 localDirection = transform.InverseTransformDirection(directionToPlayer);

            if (distanceToPlayer <= attackRange)
            {
                // 공격 애니메이션 실행
                animator.SetBool("Attack", true);

                // 투사체 발사
                if (!isAttacking) // 한 번만 시작하도록 체크
                {
                    isAttacking = true;
                    StartAttacking();  // 코루틴으로 발사 시작
                }

                // 이동 애니메이션과 추적 중지
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", false);
                navMeshAgent.ResetPath();  // 몬스터를 정지시킴
            }
            else if (distanceToPlayer <= detectionRange)
            {
                // 추적 범위 내에 플레이어가 있는 경우 플레이어를 쫓아감
                navMeshAgent.SetDestination(player.position);

                animator.SetBool("Attack", false);  // 공격 애니메이션 중지
                StopAttacking(); // 공격 멈추기

                if (localDirection.x < 0)
                {
                    // 왼쪽으로 이동하는 애니메이션
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Idle", false);
                }
                else
                {
                    // 오른쪽으로 이동하는 애니메이션
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                    animator.SetBool("Idle", false);
                }
            }
            else
            {
                // 추적 범위 밖에 플레이어가 있는 경우 멈춤
                navMeshAgent.ResetPath();

                animator.SetBool("Attack", false);  // 공격 애니메이션 중지
                StopAttacking(); // 공격 멈추기
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            // 플레이어가 없는 경우에도 Idle 상태로 유지
            animator.SetBool("Attack", false);  // 공격 애니메이션 중지
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
            StopAttacking(); // 공격 멈추기
        }
    }

    // 공격 시작 - 투사체 발사 간격에 맞춰서 계속 발사
    void StartAttacking()
    {
        StartCoroutine(FireProjectileAtInterval());
    }

    // 공격 멈추기 - 코루틴 정지
    void StopAttacking()
    {
        StopCoroutine(FireProjectileAtInterval());
        isAttacking = false;
    }

    // 코루틴을 이용한 투사체 발사
    IEnumerator FireProjectileAtInterval()
    {
        while (isAttacking)
        {
            // 현재 시간이 발사 가능 시간보다 크다면 투사체 발사
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;  // 다음 발사 시간을 설정
            }

            yield return null;  // 다음 프레임까지 대기
        }
    }

    // 투사체 발사 함수
    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // 투사체 생성
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // 플레이어 방향 계산
            Vector3 direction = (player.position - firePoint.position).normalized;

            // 투사체의 Rigidbody2D를 가져오기
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0;

                // 플레이어 방향으로 회전시키기 (2D에서 회전)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // velocity로 발사
                rb.velocity = direction * 5f;  // 필요 시 발사 속도 조정 가능
            }
            else
            {
                Debug.LogError("투사체에 Rigidbody2D가 없습니다. Rigidbody2D를 추가하세요.");
            }
        }
        else
        {
            Debug.LogError("투사체 프리팹 또는 발사 지점이 설정되지 않았습니다.");
        }
    }
}
