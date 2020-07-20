using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//IN CASE YOU COULDN'T TELL BY THE FORMAT, I DIDN'T WRITE THIS
//SOMEONE NAMED DESI QUINTANS MADE THIS SCRIPT, I JUST FOUND IT ONLINE
//I AM NOT A PARTICULARLY PRIDEFUL OR EVEN GOOD CODER, HEH
//TAKK DESI!!!!

[RequireComponent(typeof(AudioSource))]
public class audio_fadein : MonoBehaviour
{
    [SerializeField]
    private int m_FadeInTime = 10;
    private AudioSource m_AudioSource;


    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (m_AudioSource.volume < 1)
        {
            m_AudioSource.volume = m_AudioSource.volume + (Time.deltaTime / (m_FadeInTime + 1));
        }
        else
        {
            Destroy(this);
        }
    }
}
