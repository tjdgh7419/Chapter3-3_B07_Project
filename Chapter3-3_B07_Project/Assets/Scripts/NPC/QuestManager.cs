using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<int, Quest> questDict = new Dictionary<int, Quest>();

    public void AddQuest(Quest quest)
    {
        questDict.Add(quest.questId, quest);
    }
    public void ClearQuest(int id)
    {
        if(questDict.ContainsKey(id))
        {
            questDict[id].QuestClear();
            var ui = UIManager.Instance.OpenUI<QuestListPanel>();
            ui.ClearQuest(id);
            UIManager.Instance.IsOnUI = false;
            ui.gameObject.SetActive(false);
            UIManager.Instance.MouseLock();
        }
    }
}
