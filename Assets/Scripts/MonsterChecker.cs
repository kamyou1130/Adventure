using UnityEngine;

public class MonsterChecker : MonoBehaviour
{
    public GameObject portalPrefab; // 생성할 포탈 프리팹
    public Vector3 portalPosition;  // 포탈이 생성될 위치
    public string nextSceneName;    // 다음 씬의 이름

    private bool portalCreated = false; // 포탈이 이미 생성되었는지 체크

    void Update()
    {
        // "Monster" 태그를 가진 모든 오브젝트를 배열로 가져옴
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        // 모든 몬스터가 제거되었을 때
        if (monsters.Length == 0 && !portalCreated)
        {
            // 포탈이 생성되어 있지 않다면 생성
            GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
            portal.GetComponent<Portal>().nextSceneName = nextSceneName; // 포탈에 다음 씬 이름 전달
            portalCreated = true;
        }
    }
}
