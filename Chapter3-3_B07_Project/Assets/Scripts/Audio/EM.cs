using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound1;
    [SerializeField] private AudioClip clickSound2;
    [SerializeField] private AudioClip slashSound;
    [SerializeField] private AudioClip creftSound;
    [SerializeField] private AudioClip damageSound;
    //[SerializeField] private AudioClip skillSound;

    private AudioSource EMAudio;

    private void Awake()
    {
        EMAudio = GetComponent<AudioSource>();
    }

    public void SoundStop()
    {
        EMAudio.Stop();
    }

    public void Click1SoundPlay()
    {
        EMAudio.PlayOneShot(clickSound1);
    }
    public void Click2SoundPlay()
    {
        EMAudio.PlayOneShot(clickSound2);
    }

    public void SlashSoundPlay()
    {
        EMAudio.PlayOneShot(slashSound);
    }

    public void EreftSoundPlay()
    {
        EMAudio.PlayOneShot(creftSound);
    }

    public void DamageSoundPlay()
    {
        EMAudio.PlayOneShot(damageSound);
    }

    /*public void SkillSound()
    {
        EMAudio.PlayOneShot(skillSound);
    }*/
}
