using UnityEngine;

public class SceneSpecificMusic : MonoBehaviour
{
    void Start()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            // 새로운 음악을 재생하고 싶다면
            //audioManager.PlayNewClip();

            // 또는 기존 음악을 정지하고 싶다면
             audioManager.StopMusic();
        }
    }
}
