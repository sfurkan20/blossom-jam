using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public enum Sound { FIRE, FIREBALL, SLAP, PUNCH }
    public AudioSource source;
    public AudioClip[] clips;
    public static SoundSystem singleton;
    void Start()
    {
        singleton = this;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public static void setSound(Sound s)
    {
        bool oneshot = true;
        switch (s)
        {
            case Sound.FIRE:
                oneshot = false;
                break;
            case Sound.FIREBALL:
                oneshot = true;
                break;
            case Sound.PUNCH:
                oneshot = true;
                break;
            case Sound.SLAP:
                oneshot = true;
                break;
        }
        if (oneshot)
        {
            singleton.source.PlayOneShot(singleton.clips[(int)s]);
        }
        else
        {
            singleton.source.clip = singleton.clips[(int)s];
            singleton.source.Play();
        }
    }
}
