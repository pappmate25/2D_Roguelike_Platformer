using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonFX : MonoBehaviour
{
    public AudioSource src;
    public AudioClip hoverFx;
    public AudioClip clickFx;

    public void HoverSound()
    {
        src.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        src.PlayOneShot(clickFx);
    }
}
