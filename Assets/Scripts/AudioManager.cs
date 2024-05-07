using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    //Itt kell megadni amikre akarunk rakni hangot
    public AudioClip background;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void HoverSound()
    {
        musicSource.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        musicSource.PlayOneShot(clickFx);
    }
}
