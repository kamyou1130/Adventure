using UnityEngine;
using UnityEngine.AI;
using System.Collections;  // IEnumerator�� ����ϱ� ���� ���ù�

public class MonsterAI_C : MonoBehaviour
{
    public float detectionRange = 10.0f;  // ���Ͱ� �÷��̾ �ν��ϴ� ����
    public float attackRange = 3.0f;  // ���� �ִϸ��̼��� ����� �Ÿ�
    public GameObject projectilePrefab;  // ����ü ������
    public Transform firePoint;  // ����ü�� �߻�� ��ġ
    public float fireRate = 1.0f;  // ����ü �߻� ���� (�� ����)

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Transform player;  // �÷��̾��� Transform�� ������ ����
    private bool isAttacking = false;  // ���� ���� �÷���
    private float nextFireTime = 0f;  // ���� �߻� ���� �ð�

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();

        // Player �±׸� ���� ������Ʈ�� ã�Ƽ� �� Transform�� �����մϴ�.
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
            // �÷��̾���� �Ÿ� ���
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // �÷��̾� ��� ��ġ ���
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // ������ ���� ��ǥ�迡�� �÷��̾��� ���� ���
            Vector3 localDirection = transform.InverseTransformDirection(directionToPlayer);

            if (distanceToPlayer <= attackRange)
            {
                // ���� �ִϸ��̼� ����
                animator.SetBool("Attack", true);

                // ����ü �߻�
                if (!isAttacking) // �� ���� �����ϵ��� üũ
                {
                    isAttacking = true;
                    StartAttacking();  // �ڷ�ƾ���� �߻� ����
                }

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
                StopAttacking(); // ���� ���߱�

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
                StopAttacking(); // ���� ���߱�
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
            StopAttacking(); // ���� ���߱�
        }
    }

    // ���� ���� - ����ü �߻� ���ݿ� ���缭 ��� �߻�
    void StartAttacking()
    {
        StartCoroutine(FireProjectileAtInterval());
    }

    // ���� ���߱� - �ڷ�ƾ ����
    void StopAttacking()
    {
        StopCoroutine(FireProjectileAtInterval());
        isAttacking = false;
    }

    // �ڷ�ƾ�� �̿��� ����ü �߻�
    IEnumerator FireProjectileAtInterval()
    {
        while (isAttacking)
        {
            // ���� �ð��� �߻� ���� �ð����� ũ�ٸ� ����ü �߻�
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;  // ���� �߻� �ð��� ����
            }

            yield return null;  // ���� �����ӱ��� ���
        }
    }

    // ����ü �߻� �Լ�
    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // ����ü ����
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // �÷��̾� ���� ���
            Vector3 direction = (player.position - firePoint.position).normalized;

            // ����ü�� Rigidbody2D�� ��������
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0;

                // �÷��̾� �������� ȸ����Ű�� (2D���� ȸ��)
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                // velocity�� �߻�
                rb.velocity = direction * 5f;  // �ʿ� �� �߻� �ӵ� ���� ����
            }
            else
            {
                Debug.LogError("����ü�� Rigidbody2D�� �����ϴ�. Rigidbody2D�� �߰��ϼ���.");
            }
        }
        else
        {
            Debug.LogError("����ü ������ �Ǵ� �߻� ������ �������� �ʾҽ��ϴ�.");
        }
    }
}
