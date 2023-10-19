using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSetting : MonoBehaviour
{
    [SerializeField] private Button saveButton;

    [Header("Audio")]
    [SerializeField] protected Slider MasterSlider;
    [SerializeField] protected Slider MusicSlider;
    [SerializeField] protected Slider EffactSlider;

    [Header("Graphic")]
    [SerializeField] private Toggle EffactToggle;
    [SerializeField] private Toggle ShadowToggle;

    private void Start()
    {
        saveButton.onClick.AddListener(OnPreferencesSave);
        saveButton.onClick.AddListener(() => SoundManager.Instance.EffactMusic.Click2SoundPlay());
        OnPreferencesSave();
    }

    public void OnPreferencesSave()
    {
        SoundManager.Instance.SetAudioSetting(MasterSlider.value, MusicSlider.value, EffactSlider.value);
        GraphicManager.Instance.SetGraphicSetting(EffactToggle.isOn, ShadowToggle.isOn);
    }

    public void CallAudioSetting()
    {
        float[] audios = SoundManager.Instance.GetAudioSetting();
        MasterSlider.value = audios[0];
        MusicSlider.value = audios[1];
        EffactSlider.value = audios[2];
    }

    public void CallGraphicSetting()
    {
        bool[] graphic = GraphicManager.Instance.GetGraphicSetting();
        EffactToggle.isOn = graphic[0];
        ShadowToggle.isOn = graphic[1];
    }
}
