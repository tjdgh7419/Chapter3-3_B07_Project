using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public BGM BackMusic;
    public EM EffactMusic;

    private AudioSource BGM;
    private AudioSource EM;

    [Range(0f, 1f)] private static float MasterVolume = 1;
    [Range(0f, 1f)] private static float MusicVolume = 1;
    [Range(0f, 1f)] private static float EffactVolume = 1;

    private void Awake()
    {
        Instance = this;

        BGM = BackMusic.GetComponent<AudioSource>();
        EM = EffactMusic.GetComponent<AudioSource>();

        BGM.volume = MasterVolume * MusicVolume;
        EM.volume = MasterVolume * EffactVolume;
    }

    public void SetAudioSetting(float master, float music, float efffact)
    {
        MasterVolume = master;
        MusicVolume = music;
        EffactVolume = efffact;

        BGM.volume = MasterVolume * MusicVolume;
        EM.volume = MasterVolume * EffactVolume;
    }

    //0¹ø ¸¶½ºÅÍº¼·ý 1¹ø ¹ÂÁ÷º¼·ý 2¹ø ÀÌÆåÆ®º¼·ý
    public float[] GetAudioSetting()
    {
        return new float[] { MasterVolume, MusicVolume, EffactVolume };
    }
}
