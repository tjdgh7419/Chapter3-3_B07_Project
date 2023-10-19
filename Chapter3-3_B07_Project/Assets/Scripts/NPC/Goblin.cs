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
        int round = GameManager.Instance.roundManager.currentRound - 1;
        attack += round * 0.2f;
        hp += round * 0.5f;
        itemsCount[0] += round;
        itemsCount[1] += round * 2;
        base.Start();
        aiState = MobAIState.Run;
        agent.isStopped = false;
        animator.SetBool("run", true);
    }
    protected override void Update()
    {
        base.Update();
    }
}
