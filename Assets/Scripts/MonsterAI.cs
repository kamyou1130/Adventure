using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����

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
            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �÷��̾� ��� ��ġ ���
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // ������ ���� ��ǥ�迡�� �÷��̾��� ���� ���
            Vector3 localDirection = transform.InverseTransformDirection(directionToPlayer);


            if (distanceToPlayer <= detectionRange)
            {
                // ���� ���� ���� �÷��̾ �ִ� ��� �÷��̾ �Ѿư�
                navMeshAgent.SetDestination(player.position);

                if (localDirection.x < 0)
                {

                    // �������� �̵��ϴ� �ִϸ��̼�
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                }
                else
                {

                    // ���������� �̵��ϴ� �ִϸ��̼�
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
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
            // �÷��̾ ���� ��쿡�� Idle ���·� ����
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
        }
    }
}
