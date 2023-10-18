using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster
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
        attack += GameManager.Instance.roundManager.currentRound * 1f;
        hp += GameManager.Instance.roundManager.currentRound * 0.5f;
        itemsCount[0] += GameManager.Instance.roundManager.currentRound * 2;
        itemsCount[1] += GameManager.Instance.roundManager.currentRound * 4;
    }
    protected override void Update()
    {
        base.Update();
    }
}
