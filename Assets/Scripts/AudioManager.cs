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
            Destroy(instance.gameObject);  // ������ AudioManager�� ����
        }

        instance = this;  // ���� ������ ������Ʈ�� �ν��Ͻ��� ����
        DontDestroyOnLoad(gameObject);  // ���� ������ ������Ʈ�� �� ���� �ÿ��� ����
        audioSource = GetComponent<AudioSource>();

        // �� ������ �����ϰ� Ư�� ������ ���� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Ư�� ���� �ε�Ǿ��� �� ȣ��Ǵ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �̸��� "2-1", "3-1" �Ǵ� "4-1"�� ��� ���� ���
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
