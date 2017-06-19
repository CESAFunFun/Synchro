using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource sfx;
    public AudioSource bgm;
    public static SoundManager instance = null;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySound(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }
    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }
}
