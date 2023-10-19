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
		if (!GameManager.Instance.questManager.questDict.ContainsKey(quest.questId))
		{
			uiPopUp.SetAction("����Ʈ", "������ ����Ʈ�� �����Ͻðڽ��ϱ�?", YesClick, UIManager.Instance.MouseUnlock);
		}
		else
		{
			uiPopUp.SetAction("����", "�̹� ���� ����Ʈ�Դϴ�.", null, ()=>gameObject.SetActive(false));
            uiPopUp.OffCheackButton();
		}
		
    }

    public void SetQuest(Quest quest)
    {
        questInfoText.text = $"{quest.questExplan}";
        questResultText.text = $"����Ʈ ����\n{quest.questCompenExplan}";
    }

    private void YesClick()
    {
        questListPanel.SetQuestList(quest);
        GameManager.Instance.questManager.AddQuest(quest);
        gameObject.SetActive(false);
        GameManager.Instance.interactionManager.CallCloseWindow();
    }       
}
