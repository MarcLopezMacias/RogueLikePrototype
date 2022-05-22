using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioSource effects;
    public AudioSource music;

    [SerializeField]
    public AudioClip damage;
    [SerializeField]
    public AudioClip deathPlayer;
    [SerializeField]
    public AudioClip deathEnemy;

    [SerializeField]
    public AudioClip backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        effects = gameObject.GetComponent<AudioSource>();
        music = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamage()
    {
        effects.clip = damage;
        effects.Play();
    }

    public void PlayDeathPlayer()
    {
        effects.clip = deathPlayer;
        effects.Play();
    }

    public void PlayDeathEnemy()
    {
        effects.clip = deathEnemy;
        effects.Play();
    }
}
