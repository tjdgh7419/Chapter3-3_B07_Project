using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int AttackIndex { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float CoolTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float Force { get; private set; }
   // [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    
}


[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas {get; private set;}
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}
