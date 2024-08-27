using UnityEngine;
using UnityEngine.AI;

public class MonsterAI_C : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����
    public float attackRange = 3.0f;  // ���� �ִϸ��̼��� ����� �Ÿ�

    private NavMeshAgent navMeshAgent;
    private Animator animator;

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

            if (distanceToPlayer <= attackRange)
            {
                // �÷��̾ ���� ���� ���� ���� �� ���� �ִϸ��̼� ���
                animator.SetBool("Attack", true);

                // �̵� �ִϸ��̼ǰ� ���� ����
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", false);
                navMeshAgent.ResetPath();  // ���͸� ������Ŵ
            }
            else if (distanceToPlayer <= detectionRange)
            {
                // ���� ���� ���� �÷��̾ �ִ� ��� �÷��̾ �Ѿư�
                navMeshAgent.SetDestination(player.position);

                animator.SetBool("Attack", false);  // ���� �ִϸ��̼� ����

                if (localDirection.x < 0)
                {
                    // �������� �̵��ϴ� �ִϸ��̼�
                    animator.SetBool("Left", true);
                    animator.SetBool("Right", false);
                    animator.SetBool("Idle", false);
                }
                else
                {
                    // ���������� �̵��ϴ� �ִϸ��̼�
                    animator.SetBool("Right", true);
                    animator.SetBool("Left", false);
                    animator.SetBool("Idle", false);
                }
            }
            else
            {
                // ���� ���� �ۿ� �÷��̾ �ִ� ��� ����
                navMeshAgent.ResetPath();

                animator.SetBool("Attack", false);  // ���� �ִϸ��̼� ����
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
                animator.SetBool("Idle", true);
            }
        }
        else
        {
            // �÷��̾ ���� ��쿡�� Idle ���·� ����
            animator.SetBool("Attack", false);  // ���� �ִϸ��̼� ����
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Idle", true);
        }
    }
}
