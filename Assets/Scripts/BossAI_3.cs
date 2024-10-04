using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class BossAI_3 : MonoBehaviour
{
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����
    public float attackRange = 2.0f; // ���� ����
    public float dashSpeed = 10f; // ���� ���� ���ǵ�
    public float originalSpeed = 3.5f; // ���� ���ǵ�
    public float dashDuration = 2f; // ���� ���� �ð�
    public float dashCooldown = 5f; // ���� ��Ÿ��
    public float idleDuration = 1f; // Idle ��� �ð�

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject player;  // �÷��̾ ��Ÿ���� GameObject
    private bool isDashing = false; // ���� ����
    private float nextDashTime = 0f; // ���� ���� ���� �ð�

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

            // ���� ���� ���� �ִ� ���
            if (distanceToPlayer <= detectionRange)
            {
                // ���� ���� ���� �ִ� ���
                if (distanceToPlayer <= attackRange && !isDashing && Time.time >= nextDashTime)
                {
                    StartCoroutine(DashTowardsPlayer());
                    nextDashTime = Time.time + dashCooldown; // ���� ���� ���� �ð� ����
                }
                else if (!isDashing)
                {
                    // �÷��̾ ���󰡱�
                    navMeshAgent.SetDestination(player.transform.position);
                    HandleMovementAnimation(directionToPlayer);
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

    IEnumerator DashTowardsPlayer()
    {
        isDashing = true;
        animator.SetBool("Attack", true); // ���� �ִϸ��̼� ���

        // 1�� ���� Idle ���·� ���
        yield return new WaitForSeconds(idleDuration);

        // ���ǵ� ����
        navMeshAgent.speed = dashSpeed;

        // �����ϴ� ���� �÷��̾� ��ġ�� ��� ����
        float elapsedTime = 0f;
        while (elapsedTime < dashDuration)
        {
            navMeshAgent.SetDestination(player.transform.position); // �÷��̾� �������� �̵�
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // ���ǵ� ����
        navMeshAgent.speed = originalSpeed;

        animator.SetBool("Attack", false); // ���� �ִϸ��̼� ����
        isDashing = false;
    }
}
