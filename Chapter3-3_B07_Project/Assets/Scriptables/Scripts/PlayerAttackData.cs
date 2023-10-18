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
    [field: SerializeField] public float AttackDistance { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float CoolTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float AttackTime { get;  set; }
    [field: SerializeField][field: Range(0f, 3f)] public float Force { get; private set; }
    // [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    public void OnHit()
    {
        if (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            Debug.Log("공격시도1");
            if (Physics.Raycast(ray, out hit, AttackDistance))
            {
                Debug.Log("공격시도2");
                if (hit.collider.TryGetComponent(out Monster mob))
                {
                    Debug.Log("공격");
                    mob.TakeDamage(Damage);
                    AttackTime = 0f;
                }
            }
        }

    }
}


[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas {get; private set;}
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}
