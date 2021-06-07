using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioClip hit;
    static AudioSource audioSource;


    public static AudioManager Instance;

    void Start()
    {
        hit = Resources.Load<AudioClip>("Audios/hit");
        audioSource = GetComponent<AudioSource>();
    }

    public static void Play(string clip) {
        switch(clip) {
            case "hit":
                audioSource.PlayOneShot(hit);
                break;
            default:
                break;
        }
    }
}
