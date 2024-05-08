using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    //Itt kell megadni amikre akarunk rakni hangot
    [Header("AUDIO CLIPS")]
    public AudioClip background;
    public AudioClip bow;
    public AudioClip character_hurt;
    public AudioClip death;
    public AudioClip slimeHit;
    public AudioClip slimeHop;
    public AudioClip dodge;
    public AudioClip JUMP;
    public AudioClip ladderClimb;
    public AudioClip landing;
    public AudioClip walking;
    public AudioClip gameStart;
    public AudioClip selectHover;
    public AudioClip selected;

    public float delay = 0f;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlayOneShot(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        
    }
    public void PlayDelayed(AudioClip clip)
    {
        SFXSource.clip= clip;
        SFXSource.PlayDelayed(delay);

    }

}
