using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource BackMusic;
    [SerializeField] private AudioSource EffactMusic;

    [Range(0f, 1f)] private static float MasterVolume = 1;
    [Range(0f, 1f)] private static float MusicVolume = 1;
    [Range(0f, 1f)] private static float EffactVolume = 1;

    private void Awake()
    {
        Instance = this;

        BackMusic.volume = MasterVolume * MusicVolume;
        EffactMusic.volume = MasterVolume * EffactVolume;
    }

    public void SetAudioSetting(float master, float music, float efffact)
    {
        MasterVolume = master;
        MusicVolume = music;
        EffactVolume = efffact;

        BackMusic.volume = MasterVolume * MusicVolume;
        EffactMusic.volume = MasterVolume * EffactVolume;
    }

    //0�� �����ͺ��� 1�� �������� 2�� ����Ʈ����
    public float[] GetAudioSetting()
    {
        return new float[] { MasterVolume, MusicVolume, EffactVolume };
    }
}
