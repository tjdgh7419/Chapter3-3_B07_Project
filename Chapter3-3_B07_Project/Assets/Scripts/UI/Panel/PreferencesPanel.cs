using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreferencesPanel : StartUIBase
{
    [SerializeField] private Button graphicButton;
    [SerializeField] private Button audioButton;

    [Header("Panel")]
    [SerializeField] private GameObject graphicPanel;
    [SerializeField] private GameObject audioPanel;

    [Header("Save")]
    [SerializeField] private SaveSetting saveSetting;

    private void Start()
    {
        graphicButton.onClick.AddListener(OnGraphicPanel);
        audioButton.onClick.AddListener(OnAudioPanel);
        saveSetting.CallAudioSetting();
        saveSetting.CallGraphicSetting();
    }

    protected override void Close()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            base.Close();
        }
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            gameObject.SetActive(false);
            UIManager.Instance.OpenUI<PausePanel>();
            SoundManager.Instance.EffactMusic.Click1SoundPlay();
        }
        saveSetting.CallAudioSetting();
        saveSetting.CallGraphicSetting();
    }

    private void OnGraphicPanel()
    {
        graphicPanel.SetActive(true);
        audioPanel.SetActive(false);
        SoundManager.Instance.EffactMusic.Click2SoundPlay();
    }
    
    private void OnAudioPanel()
    {
        graphicPanel.SetActive(false);
        audioPanel.SetActive(true);
        SoundManager.Instance.EffactMusic.Click2SoundPlay();
    }
}
