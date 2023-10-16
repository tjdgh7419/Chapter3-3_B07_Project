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
        if (SceneManager.GetActiveScene().name == "UI_DEV_Scene")
        {
            gameObject.SetActive(false);
            UIManager.Instance.OpenUI<PausePanel>();
        }
        saveSetting.CallAudioSetting();
        saveSetting.CallGraphicSetting();
    }

    private void OnGraphicPanel()
    {
        graphicPanel.SetActive(true);
        audioPanel.SetActive(false);
    }
    
    private void OnAudioPanel()
    {
        graphicPanel.SetActive(false);
        audioPanel.SetActive(true);
    }
}
