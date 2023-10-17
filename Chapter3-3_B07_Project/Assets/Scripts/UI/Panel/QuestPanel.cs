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
        SetQuest("������� 10���� ���","");
    }
    void OpenUI_Quest()
    {
        var uiPopUp = UIManager.Instance.OpenUI<UIPopUp>();
        uiPopUp.SetAction("����Ʈ", "������ ����Ʈ�� �����Ͻðڽ��ϱ�?", asdfasdf);
    }

    public void SetQuest(string info, string result)
    {
        questInfoText.text = $"{info}";
        questResultText.text = $"����Ʈ ����\n{result}";
    }

    private void asdfasdf()
    {
        questListPanel.SetQuestList(questInfoText.text);
        gameObject.SetActive(false);
        GameManager.Instance.interactionManager.CallCloseWindow();
    }
}
