using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
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
    }
    protected override void Update()
    {
        base.Update();
    }
}
