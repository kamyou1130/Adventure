using UnityEngine;

public class SceneSpecificMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            // ���ο� ������ ����ϰ� �ʹٸ�
            //audioManager.PlayNewClip();

            // �Ǵ� ���� ������ �����ϰ� �ʹٸ�
             audioManager.StopMusic();
        }
    }
}
