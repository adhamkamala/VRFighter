
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- AudioSource ------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- AudioSource ------------")]
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip rightPunch;
    public AudioClip LeftPunch;

    private void Start()
    {
        musicSource.clip = background2;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
