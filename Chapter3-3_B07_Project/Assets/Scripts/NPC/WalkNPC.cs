using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class WalkNPC : NPC
{
    [Header("Walk")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderTime;
    public float maxWanderTime;
    public float safeDistance;
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
        npcSO.npcType = 1;
        _name.text = npcSO.npcName;
        npcAI = NPCAIState.Walk;
        talk.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }
    protected override void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        switch (npcAI)
        {
            case NPCAIState.Idle:
                {
                    PassiveUpdate();
                }
                break;
            case NPCAIState.Interact:
                {
                    InteractUpdate();
                }
                break;
            case NPCAIState.Walk:
                {
                    agent.speed = npcSO.speed;
                    agent.isStopped = false;
                    WalkUpdate();
                    if (playerDistance < safeDistance)
                    {
                        if (playerDistance > detectDistance || !IsPlayerInFieldOfView())
                        {
                            agent.isStopped = false;
                            NavMeshPath path = new NavMeshPath();
                            if (agent.CalculatePath(player.transform.position, path))
                            {
                                agent.SetDestination(player.transform.position + new Vector3(2f, 0, 1f));
                            }
                        }
                        else if (playerDistance < detectDistance)
                        {
                            CanMove(false);
                            agent.speed = 0;
                            npcAI = NPCAIState.Interact;
                        }
                    }
                }
                break;
        }
        
    }
    private void WalkUpdate()
    {
        if (npcAI == NPCAIState.Walk && agent.remainingDistance < 0.1f)
        {
            npcAI = NPCAIState.Idle;
            CanMove(false);
            agent.SetDestination(GetFleeLocation());
            Invoke("WalkToNewLocation", Random.Range(minWanderTime, maxWanderTime));
        }
    }
    void WalkToNewLocation()
    {
        CanMove(true);
        npcAI = NPCAIState.Walk;
        agent.SetDestination(GetWanderLocation());
    }


    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - player.transform.position, transform.position + targetPos);
    }
    void CanMove(bool move)
    {
        npcSO.canMove = move;
        agent.isStopped = !move;
        animator.SetBool("Move", npcSO.canMove);
    }
}
