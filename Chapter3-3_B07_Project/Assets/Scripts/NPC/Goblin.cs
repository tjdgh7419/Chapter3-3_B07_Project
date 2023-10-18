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
    }
    protected override void Update()
    {
        base.Update();
    }
}