using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class Monster : MonoBehaviour
{
    public enum MobAIState
    {
        Idle,
        Attacking,
        Run
    }
    public int type;
    public string mobName;

    public float attack;
    public float attackDelay;
    public float attackTime;
    public float attackDistance;
    public float hp;

    public float speed;
    public float detectDistance;
    public float playerDistance;

    Transform castlePos;
    protected GameObject player;
    protected NavMeshAgent agent;
    protected Animator animator;

    private MobAIState aiState;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.gameObject.name = mobName;
        castlePos = GameManager.Instance.monsterManager.castlePos;
        Debug.Log(castlePos.position);
        agent.SetDestination(castlePos.position);
        agent.speed = speed;
        agent.isStopped = false;
        animator.SetBool("run", true);
    }
}
