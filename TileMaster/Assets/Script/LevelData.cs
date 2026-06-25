using UnityEngine;

public class LevelData : MonoBehaviour
{
    public AudioClip levelBackgroundAudioClip;

    void Start()
    {
        GeneralSoundController.Instance.PlaySoundLoop(levelBackgroundAudioClip);
    }
}
