using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3 originalScale;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
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


            if (distanceToPlayer <= detectionRange)
            {
                // 추적 범위 내에 플레이어가 있는 경우 플레이어를 쫓아감
                navMeshAgent.SetDestination(player.position);

                if (localDirection.x < 0)
                {

                    // 왼쪽으로 이동하는 애니메이션
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                }
                else
                {

                    // 오른쪽으로 이동하는 애니메이션
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
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
            // 플레이어가 없는 경우에도 Idle 상태로 유지
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
        }
    }
}
