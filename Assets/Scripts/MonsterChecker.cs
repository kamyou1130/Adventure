using UnityEngine;

public class MonsterChecker : MonoBehaviour
{
    public GameObject portalPrefab; // ������ ��Ż ������
    public Vector3 portalPosition;  // ��Ż�� ������ ��ġ
    public string nextSceneName;    // ���� ���� �̸�

    private bool portalCreated = false; // ��Ż�� �̹� �����Ǿ����� üũ

    void Update()
    {
        // "Monster" �±׸� ���� ��� ������Ʈ�� �迭�� ������
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        // ��� ���Ͱ� ���ŵǾ��� ��
        if (monsters.Length == 0 && !portalCreated)
        {
            // ��Ż�� �����Ǿ� ���� �ʴٸ� ����
            GameObject portal = Instantiate(portalPrefab, portalPosition, Quaternion.identity);
            portal.GetComponent<Portal>().nextSceneName = nextSceneName; // ��Ż�� ���� �� �̸� ����
            portalCreated = true;
        }
    }
}
