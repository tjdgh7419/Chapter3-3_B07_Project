using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject buttons;
    [SerializeField] private Button startButton;
    [SerializeField] private Button PreferencesButton;
    [SerializeField] private Button ProducerButton;

    private void Start()
    {
        startButton.onClick.AddListener(OpenUI_Start);
        PreferencesButton.onClick.AddListener(OpenUI_Preferences);
        ProducerButton.onClick.AddListener(OpenUI_Producer);
        UIManager.Instance.OpenUI<ButtonsPanel>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void OpenUI_Start()
    {
        LoadSceneManager.LoadScene("UI_DEV_Scene");
    }

    void OpenUI_Preferences()
    {
        UIManager.Instance.OpenUI<PreferencesPanel>();
        buttons.SetActive(false);
    }

    void OpenUI_Producer()
    {
        UIManager.Instance.OpenUI<ProducerPanel>();
        buttons.SetActive(false);
    }
}
