using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSound : MonoBehaviour
{
    
    public Slider bgm, sfx;
    public static float volume;
    public static float sfxVolume;
    public AudioSource backgroundAudio, rockhit, dmgSrc;
    
   public void Setsound()
    {
        volume = bgm.value;
        sfxVolume = sfx.value;
        backgroundAudio.volume = volume;
        rockhit.volume = sfxVolume;
        dmgSrc.volume = sfxVolume;
        Debug.Log(volume );
        Debug.Log(sfxVolume);
        
    }
    
}
