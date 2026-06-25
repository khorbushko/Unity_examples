using UnityEngine;

public class GeneralSoundController : MonoBehaviour
{
    public static GeneralSoundController Instance;

    public AudioSource audioSourceOnce;
    public AudioSource audioSourceLoop;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (audioSourceOnce != null)
        {
            audioSourceOnce.volume = PlayerPrefs.GetFloat("soundEffectVolume", 1.0f);
        }

        if (audioSourceLoop != null)
        {
            audioSourceLoop.volume = PlayerPrefs.GetFloat("soundEffectVolume", 1.0f);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSourceOnce.loop = false;
        audioSourceOnce.PlayOneShot(clip);
    }

    public void PlaySoundLoop(AudioClip clip)
    {
        audioSourceLoop.loop = true;
        audioSourceLoop.clip = clip;
        audioSourceLoop.Play();
    }

    public void StopLoopSound()
    {
        audioSourceLoop.Stop();
    }
}
