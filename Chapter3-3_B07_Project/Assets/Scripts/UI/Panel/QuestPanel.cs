using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : GameUIBase
{
    [SerializeField] private Button acceptButton;
    public Quest quest;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI questInfoText;
    [SerializeField] private TextMeshProUGUI questResultText;

    [Header("List")]
    [SerializeField] private QuestListPanel questListPanel;

    private int questCount = 0;
    protected override void Awake()
    {
        base.Awake();
        acceptButton.onClick.AddListener(OpenUI_Quest);
    }
    private void OnEnable()
    {
        SetQuest(quest);
    }
    void OpenUI_Quest()
    {
        var uiPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        SoundManager.Instance.EffactMusic.Click2SoundPlay();
        uiPopUp.SetAction("Äù½ºÆ®", questListPanel.SetQuestList(quest), null, Click);
        uiPopUp.OffCheackButton();
    }

    public void SetQuest(Quest quest)
    {
        if (quest == null)
            return;
        questInfoText.text = $"{quest.questExplan}";
        questResultText.text = $"Äù½ºÆ® º¸»ó\n{quest.questCompenExplan}";
    }

    private void Click()
    {
        gameObject.SetActive(false);
        GameManager.Instance.interactionManager.CallCloseWindow();
    }
    protected override void Close()
    {
        base.Close();
        GameManager.Instance.interactionManager.CallCloseWindow();
    }
}
