using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _audios;
    
    public static SoundManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        _audioSource = GameObject.FindAnyObjectByType<AudioSource>();
        _audioSource.volume = 0.1f;
    }

    public void AudioPlay(int value)
    {
        _audioSource.clip = _audios[value];
        _audioSource.Play();
    }

    public void AudioVolume(float volumeValue)
    {
        _audioSource.volume = volumeValue;
    }
    
}
