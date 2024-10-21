using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public AudioClip newClip;  // 새로운 음악 클립 (필요시 설정)
    private static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 변경되더라도 파괴되지 않게 설정
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);  // 중복된 AudioManager가 있을 경우 파괴
        }
    }

    //public void PlayNewClip()
    //{
    //    if (newClip != null)
    //    {
    //        audioSource.clip = newClip;
    //        audioSource.Play();
    //    }
    //}

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
