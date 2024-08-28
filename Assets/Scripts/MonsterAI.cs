using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // �÷��̾ ��Ÿ���� GameObject

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

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

            // ������ ���� ��ǥ�迡�� �÷��̾��� ���� ���
            Vector3 localDirection = transform.InverseTransformDirection(directionToPlayer);

            if (distanceToPlayer <= detectionRange)
            {
                // ���� ���� ���� �÷��̾ �ִ� ��� �÷��̾ �Ѿư�
                navMeshAgent.SetDestination(player.transform.position);

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

            // ���� �÷��̾ ������ ������� ���, �ٽ� ã�� �õ�
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}
