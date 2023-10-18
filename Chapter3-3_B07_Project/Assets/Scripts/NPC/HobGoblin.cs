using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class HobGoblin : Monster
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
    }
    protected override void SetDestination()
    {
        agent.SetDestination(GameManager.Instance.monsterManager.hobSpanPos.position);
    }
}
