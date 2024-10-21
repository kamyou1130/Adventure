using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public AudioClip newClip;  // ���ο� ���� Ŭ�� (�ʿ�� ����)
    private static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // ���� ����Ǵ��� �ı����� �ʰ� ����
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);  // �ߺ��� AudioManager�� ���� ��� �ı�
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
