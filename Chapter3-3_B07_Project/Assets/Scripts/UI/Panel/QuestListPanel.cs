using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestListPanel : GameUIBase
{
    [SerializeField] private Button giveUpQuest1;
    [SerializeField] private Button giveUpQuest2;
    [SerializeField] private Button giveUpQuest3;

    [Header("QuestText")]
    [SerializeField] private TextMeshProUGUI[] QuestText = new TextMeshProUGUI[3];
    List<Quest> questList = new List<Quest>();
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        giveUpQuest1.onClick.AddListener(() => GiveupQuest(0));
        giveUpQuest2.onClick.AddListener(() => GiveupQuest(1));
        giveUpQuest3.onClick.AddListener(() => GiveupQuest(2));
        for(int i = count; i < QuestText.Length; i++)
        {
            QuestText[i].gameObject.SetActive(false);
        }
    }

    public string SetQuestList(Quest quest)
    {
        if(count == 3 || GameManager.Instance.questManager.questDict.ContainsKey(quest.questId))
        {
            return "퀘스트 추가에 실패하였습니다.";
        }
        GameManager.Instance.questManager.AddQuest(quest);
        questList.Add(quest);
        QuestText[count].text = quest.questExplan;
        QuestText[count].gameObject.SetActive(true);

        count++;
        return "퀘스트를 추가 하였습니다.";
    }

    private void GiveupQuest(int i)
    {
        GameManager.Instance.questManager.questDict.Remove(questList[i].questId);
        questList.RemoveAt(i);
        for (int j = 0; j < questList.Count; j++)
        {
            QuestText[j].text = questList[j].questExplan;
        }
        count--;
        QuestText[count].text = "";
        QuestText[count].gameObject.SetActive(false);
    }
    public void ClearQuest(int id)
    {
        questList.Remove(GameManager.Instance.questManager.questDict[id]);
        GameManager.Instance.questManager.questDict.Remove(id);
        for (int j = 0; j < questList.Count; j++)
        {
            QuestText[j].text = questList[j].questExplan;
        }
        count--;
        QuestText[count].text = "";
        QuestText[count].gameObject.SetActive(false);
    }
}
