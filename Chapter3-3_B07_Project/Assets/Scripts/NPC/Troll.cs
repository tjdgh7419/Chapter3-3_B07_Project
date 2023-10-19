using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Monster
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        aiState = MobAIState.Idle;
        agent.isStopped = true;
    }
    protected override void Update()
    {
        base.Update();
        hpBar.fillAmount = hp / maxHp;
    }
    protected override void SetDestination()
    {
        agent.SetDestination(GameManager.Instance.monsterManager.trollSpanPos.position);
    }
}
