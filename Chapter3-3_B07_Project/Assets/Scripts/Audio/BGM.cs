using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] private AudioClip StartSceneSound;
    [SerializeField] private AudioClip WaveOnSound;
    [SerializeField] private AudioClip WaveOffSound;
    [SerializeField] private AudioClip ProducerSound;


    private AudioSource BGMAudio;

    private void Awake()
    {
        BGMAudio = GetComponent<AudioSource>();
    }

    public void StopBGM()
    {
        BGMAudio.Stop();
    }

    public void StartScene()
    {
        BGMAudio.clip = StartSceneSound;
        BGMAudio.Play();
    }
    
    public void Producer()
    {
        BGMAudio.clip = ProducerSound;
        BGMAudio.Play();
    }

    public void WaveOn()
    {
        BGMAudio.clip = WaveOnSound;
        BGMAudio.Play();
    }
    
    public void WaveOff()
    {
        BGMAudio.clip = WaveOffSound;
        BGMAudio.Play();
    }
}
