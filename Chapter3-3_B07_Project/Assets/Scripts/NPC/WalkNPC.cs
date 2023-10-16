using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkNPC : NPC
{
    [Header("Walk")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderTime;
    public float maxWanderTime;
    private NavMeshAgent agent;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        agent= GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if(npcAI == NPCAIState.Walk)
        {
            agent.speed = npcSO.speed;
            agent.isStopped = false;
        }
    }
    private void WalkUpdate()
    {
        if (npcAI == NPCAIState.Walk && agent.remainingDistance < 0.1f)
        {
            npcAI = NPCAIState.Idle;
            Invoke("WalkToNewLocation", Random.Range(minWanderTime, maxWanderTime));
        }
    }
}
