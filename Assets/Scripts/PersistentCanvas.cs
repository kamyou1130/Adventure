using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
    private static PersistentCanvas instance;

    void Awake()
    {
        // 싱글턴 패턴을 사용하여 하나의 인스턴스만 유지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 오브젝트 삭제
        }
    }
}
