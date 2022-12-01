using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySound : MonoBehaviour
{

    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = SetSound.volume;
        Debug.Log(SetSound.volume);
    }
}
