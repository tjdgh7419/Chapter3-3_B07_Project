using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Monster;

public class EGoblin : Monster
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        int round = GameManager.Instance.roundManager.currentRound - 1;
        attack += round * 0.5f;
        hp += round * 0.5f;
        itemsCount[0] += round * 3;
        itemsCount[1] += round * 3;
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
