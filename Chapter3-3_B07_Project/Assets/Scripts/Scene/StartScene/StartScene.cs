using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button PreferencesButton;
    [SerializeField] private Button ProducerButton;

    private void Start()
    {
        startButton.onClick.AddListener(OpenUI_Start);
        PreferencesButton.onClick.AddListener(OpenUI_Preferences);
        ProducerButton.onClick.AddListener(OpenUI_Producer);
        UIManager.Instance.OpenUI<ButtonsPanel>();
    }

    void OpenUI_Start()
    {
        
    }

    void OpenUI_Preferences()
    {
        UIManager.Instance.OpenUI<PreferencesPanel>();
        UIManager.Instance.CloseUI<ButtonsPanel>();
    }
    void OpenUI_Producer()
    {
        UIManager.Instance.OpenUI<ProducerPanel>();
        UIManager.Instance.CloseUI<ButtonsPanel>();
    }
}
