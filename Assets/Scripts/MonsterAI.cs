using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 10.0f;  // 몬스터가 플레이어를 인식하는 범위

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // 플레이어를 나타내는 GameObject

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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

            // 몬스터의 로컬 좌표계에서 플레이어의 방향 계산
            Vector3 localDirection = transform.InverseTransformDirection(directionToPlayer);

            if (distanceToPlayer <= detectionRange)
            {
                // 추적 범위 내에 플레이어가 있는 경우 플레이어를 쫓아감
                navMeshAgent.SetDestination(player.transform.position);

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

            // 만약 플레이어가 씬에서 사라졌을 경우, 다시 찾기 시도
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
