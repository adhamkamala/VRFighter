using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- AudioSource ------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- AudioSource ------------")]
    public AudioClip background1;
    public AudioClip background2;
    public AudioClip RightPunch;
    public AudioClip LeftPunch;

    public bool isNormal=false;

    private void Start()
    {
        if (isNormal)
        {
            musicSource.clip = background2;
            musicSource.volume = 0.1f;
            musicSource.Play();
        }
    
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}