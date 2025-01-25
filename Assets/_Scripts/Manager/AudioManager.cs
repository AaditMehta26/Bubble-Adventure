using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : Singleton<AudioManager>
{
    [FormerlySerializedAs("_musicSource")] [SerializeField] private AudioSource musicSource;
    [FormerlySerializedAs("_soundsSource")] [SerializeField] private AudioSource soundsSource;
    
    
    public AudioClip musicClip;
    public AudioClip deathClip;
    public AudioClip dashClip;
    public AudioClip uiPopClip;
    public void Start() {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1) {
        soundsSource.transform.position = pos;
        PlaySound(clip, vol);
    }

    public void PlaySound(AudioClip clip, float vol = 1) {
        soundsSource.PlayOneShot(clip, vol);
    }
}
