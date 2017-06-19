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
    
    public void PlaySFX(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
        if(Input.anyKey)
        {
            sfx.Stop();
        }
    }
    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }
}
