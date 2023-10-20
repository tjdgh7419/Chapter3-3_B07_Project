using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questId;
    public string questCompenExplan;
    public string questName;
    public string questExplan;
    public ItemData[] questCompenItems;
    public int[] questCompenItemsCount;
    

    private void Awake()
    {
        questCompenExplan = $"º¸»ó : Å©¸®½ºÅ» {questCompenItemsCount[0]}°³, °ñµå {questCompenItemsCount[1]}";
    }
    public void QuestClear()
    {
        for (int i = 0; i < questCompenItems.Length; i++)
        {
            for (int j = 0; j < questCompenItemsCount[i]; j++)
            {
                GameManager.Instance.inventory.AddItem(questCompenItems[i]);
            }
        }
    }
}
