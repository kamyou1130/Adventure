using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI_3 : MonoBehaviour
{
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위
    public float attackRange = 2.0f; // 공격 범위
    public float dashSpeed = 10f; // 돌진 시의 스피드
    public float originalSpeed = 3.5f; // 원래 스피드
    public float dashDuration = 2f; // 돌진 지속 시간
    public float dashCooldown = 5f; // 돌진 쿨타임
    public float idleDuration = 1f; // Idle 대기 시간

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // 플레이어를 나타내는 GameObject
    private bool isDashing = false; // 돌진 상태
    private float nextDashTime = 0f; // 다음 돌진 가능 시간

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

            // 추적 범위 내에 있는 경우
            if (distanceToPlayer <= detectionRange)
            {
                // 공격 범위 내에 있는 경우
                if (distanceToPlayer <= attackRange && !isDashing && Time.time >= nextDashTime)
                {
                    StartCoroutine(DashTowardsPlayer());
                    nextDashTime = Time.time + dashCooldown; // 다음 돌진 가능 시간 설정
                }
                else if (!isDashing)
                {
                    // 플레이어를 따라가기
                    navMeshAgent.SetDestination(player.transform.position);
                    HandleMovementAnimation(directionToPlayer);
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

    IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        animator.SetBool("Attack", true); // 공격 애니메이션 재생

        // 1초 동안 Idle 상태로 대기
        yield return new WaitForSeconds(idleDuration);

        // 스피드 증가
        navMeshAgent.speed = dashSpeed;

        // 돌진하는 동안 플레이어 위치를 계속 추적
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            navMeshAgent.SetDestination(player.transform.position); // 플레이어 방향으로 이동
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }

        // 스피드 복원
        navMeshAgent.speed = originalSpeed;

        animator.SetBool("Attack", false); // 공격 애니메이션 중지
        isDashing = false;
    }
}
