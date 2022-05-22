using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioSource source;

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
        source = gameObject.GetComponent<AudioSource>();
        source.clip = backgroundMusic;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamage()
    {
        source.PlayOneShot(damage);
    }

    public void PlayDeathPlayer()
    {
        source.PlayOneShot(deathPlayer);
    }

    public void PlayDeathEnemy()
    {
        source.PlayOneShot(deathEnemy);
    }
}
