using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class Goblin : Monster
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        aiState = MobAIState.Run;
        agent.isStopped = false;
        animator.SetBool("run", true);
        attack += GameManager.Instance.roundManager.currentRound * 0.5f;
        hp += GameManager.Instance.roundManager.currentRound * 1f;
        itemsCount[0] += GameManager.Instance.roundManager.currentRound;
        itemsCount[1] += GameManager.Instance.roundManager.currentRound * 2;
    }
    protected override void Update()
    {
        base.Update();
    }
}
