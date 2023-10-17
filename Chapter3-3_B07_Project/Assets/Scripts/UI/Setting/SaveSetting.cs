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
    }

    public void OnPreferencesSave()
    {
        UIManager.Instance.SetAudioSetting(MasterSlider.value, MusicSlider.value, EffactSlider.value);
        UIManager.Instance.SetGraphicSetting(EffactToggle.isOn, ShadowToggle.isOn);
    }

    public void CallAudioSetting()
    {
        float[] audios = UIManager.Instance.GetAudioSetting();
        MasterSlider.value = audios[0];
        MusicSlider.value = audios[1];
        EffactSlider.value = audios[2];
    }

    public void CallGraphicSetting()
    {
        bool[] graphic = UIManager.Instance.GetGraphicSetting();
        EffactToggle.isOn = graphic[0];
        ShadowToggle.isOn = graphic[1];
    }
}