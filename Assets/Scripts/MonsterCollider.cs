using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // �浹�� ������Ʈ�� �±װ� "Arrow"���� Ȯ��
        if (other.CompareTag("Arrow"))
        {
            // ���� ������Ʈ �� �浹 ������Ʈ ����
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
