using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // 충돌한 오브젝트의 태그가 "Arrow"인지 확인
        if (other.CompareTag("Arrow"))
        {
            // 몬스터 오브젝트 및 충돌 오브젝트 제거
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
