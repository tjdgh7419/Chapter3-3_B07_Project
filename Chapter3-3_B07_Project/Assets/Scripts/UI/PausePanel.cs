using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : GameUIBase
{
    [Header("Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button preferencesButton;

    private void Start()
    {
        continueButton.onClick.AddListener(Close);
        quitButton.onClick.AddListener(OpenUI_Quit);
        preferencesButton.onClick.AddListener(OpenUI_Preferences);
    }

    void OpenUI_Quit()
    {
        var uiPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        uiPopUp.SetAction("������","����ȭ������ ���ư��ðڽ��ϱ�?\n(��������� ������ ��� ������ϴ�)",() => LoadSceneManager.LoadScene("StartScene"));
        
    }
    void OpenUI_Preferences()
    {
        UIManager.Instance.OpenUI<PreferencesPanel>();
        Close();
    }
}
