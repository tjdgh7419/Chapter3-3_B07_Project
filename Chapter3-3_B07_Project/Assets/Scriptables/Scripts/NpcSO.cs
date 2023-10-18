using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultNPCData", menuName = "NPC/Default")]
[System.Serializable]
public class NpcSO : ScriptableObject
{
    [Header("NPC Info")]
    public int npcType;
    public float speed;
    public string npcName;
    public string[] npcLine;
    public bool interact;
    public bool canMove;
}
