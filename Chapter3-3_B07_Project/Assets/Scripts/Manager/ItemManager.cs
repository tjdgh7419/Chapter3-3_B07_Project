using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData HpPotion;
    public ItemData MpPotion;
    public ItemData Sword;
    public ItemData crystal;
    public ItemData Gold;
   
    void Start()
    {
        GameManager.Instance.itemManager = this;
    }

  
}
