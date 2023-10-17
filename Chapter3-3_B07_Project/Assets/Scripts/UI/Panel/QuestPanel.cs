using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : GameUIBase
{
    [SerializeField] private Button acceptButton;

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
        SetQuest("웨어울프 10마리 잡기","");
    }
    void OpenUI_Quest()
    {
        var uiPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        uiPopUp.SetAction("퀘스트", "정말로 퀘스트를 수락하시겠습니까?", asdfasdf);
    }

    public void SetQuest(string info, string result)
    {
        questInfoText.text = $"{info}";
        questResultText.text = $"퀘스트 보상\n{result}";
    }

    private void asdfasdf()
    {
        questListPanel.SetQuestList(questInfoText.text);
        gameObject.SetActive(false);
        GameManager.Instance.interactionManager.CallCloseWindow();
    }
}
