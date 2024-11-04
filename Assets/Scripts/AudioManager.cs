using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);  // 기존의 AudioManager를 삭제
        }

        instance = this;  // 새로 생성된 오브젝트를 인스턴스로 지정
        DontDestroyOnLoad(gameObject);  // 새로 생성된 오브젝트를 씬 변경 시에도 유지
        audioSource = GetComponent<AudioSource>();

        // 씬 변경을 감지하고 특정 씬에서 음악 재생
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 특정 씬이 로드되었을 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름이 "2-1", "3-1" 또는 "4-1"일 경우 음악 재생
        if (scene.name == "2-1" || scene.name == "3-1" || scene.name == "4-1")
        {
            PlayMusic();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
