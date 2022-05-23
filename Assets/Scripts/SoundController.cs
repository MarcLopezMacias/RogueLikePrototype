using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{

    [SerializeField]
    public AudioMixer mixer;

    [SerializeField]
    public AudioSource musicSource, effectsSource;

    [SerializeField]
    public AudioClip damage, deathPlayer, deathEnemy;

    [SerializeField]
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamage()
    {
        effectsSource.PlayOneShot(damage);
    }

    public void PlayDeathPlayer()
    {
        effectsSource.PlayOneShot(deathPlayer);
    }

    public void PlayDeathEnemy()
    {
        effectsSource.PlayOneShot(deathEnemy);
    }

    public void MuteMusic()
    {
        musicSource.volume = 0;
    }

    public void MuteEffects()
    {
        effectsSource.volume = 0;
    }
}
