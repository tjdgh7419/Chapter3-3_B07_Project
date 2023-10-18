using UnityEngine;
using UnityEngine.UI;

public class PausePanel : GameUIBase
{
    [Header("Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button preferencesButton;

    protected override void Awake()
    {
        base.Awake();
        continueButton.onClick.AddListener(Close);
        quitButton.onClick.AddListener(OpenUI_Quit);
        preferencesButton.onClick.AddListener(OpenUI_Preferences);
    }

    void OpenUI_Quit()
    {
        var uiPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        SoundManager.Instance.EffactMusic.Click2SoundPlay();
        uiPopUp.SetAction("������","����ȭ������ ���ư��ðڽ��ϱ�?\n(��������� ������ ��� ������ϴ�)",() => LoadSceneManager.LoadScene("StartScene"), () => UIManager.Instance.MouseUnlock());
    }

    void OpenUI_Preferences()
    {
        UIManager.Instance.OpenUI<PreferencesPanel>();
        gameObject.SetActive(false);
    }

    protected override void Close()
    {
        base.Close();
        Time.timeScale = 1;
    }
}
