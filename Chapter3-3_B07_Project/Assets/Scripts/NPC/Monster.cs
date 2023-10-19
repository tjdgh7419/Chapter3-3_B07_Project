using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public enum MobAIState
    {
        Idle,
        Battle,
        Run
    }
    public int type;
    public string mobName;

    public float attack;
    public float attackDelay;
    protected float attackTime;
    public float attackDistance;
    public float hp;
    protected bool dead;
    public float speed;
    public float detectDistance;
    protected float playerDistance;
    
    protected GameObject player;
    protected NavMeshAgent agent;
    protected Animator animator;

    protected MobAIState aiState;

    protected float fieldOfView = 120f;

    [SerializeField] protected ItemData[] items;
    [SerializeField] protected int[] itemsCount;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.gameObject.name = mobName;
        agent.speed = speed;
        SetDestination();
    }

    protected virtual void Update()
    {
        playerDistance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);
        switch (aiState)
        {
            case MobAIState.Idle:
                if (playerDistance < detectDistance)
                {
                    aiState = MobAIState.Battle;
                    agent.isStopped = false;
                    animator.SetBool("run", true);
                }
                break;
            case MobAIState.Battle:
                AttackingUpdate();
                break;
            case MobAIState.Run:
                if (playerDistance < detectDistance)
                {
                    aiState = MobAIState.Battle;
                }
                if (Vector3.Distance(this.transform.position, agent.destination) <= 0.1f)
                {
                    Debug.Log("발동");
                    agent.isStopped = true;
                    animator.SetBool("run", false);
                    aiState = MobAIState.Idle;
                }
                break;
        }
    }
    private void AttackingUpdate()
    {
        if (playerDistance > attackDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(player.transform.position, path) && playerDistance < detectDistance)
            {
                agent.SetDestination(player.transform.position);
                this.transform.LookAt(player.transform.position);
            }
            else if (playerDistance > detectDistance)
            {
                Debug.Log("발동2");
                aiState = MobAIState.Run;
                SetDestination();
            }
        }
        else
        {
            agent.isStopped = true;
            if (Time.time - attackTime > attackDelay)
            {
                this.transform.LookAt(player.transform.position);
                attackTime = Time.time;
                OnHit();
				animator.SetTrigger("attack");
            }
        }
    }
    void OnHit()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, attackDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                var pu = UIManager.Instance.OpenUI<PlayerUI>();
                pu.TakeDamage(attack);
            }
        }
    }
    private bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        animator.SetTrigger("damage");
        StartCoroutine(SetDetectDistance());
        if(hp <= 0f && !dead)
        {
            dead = true;
            animator.SetTrigger("dead");
            GiveItems();
            //GameManager.Instance.questManager.ClearQuest(type);
            Invoke("ActiveFalse", 2f);
        }
    }
    IEnumerator SetDetectDistance()
    {
        float detect = detectDistance;
        detectDistance = 30f;
        yield return new WaitForSeconds(8f);
        detectDistance = detect;
    }
    protected virtual void SetDestination()
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(GameManager.Instance.monsterManager.castlePos.position, path))
        {
            this.transform.LookAt(GameManager.Instance.monsterManager.castlePos.position);
            agent.SetDestination(GameManager.Instance.monsterManager.castlePos.position);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Castle")
        {
            if (collision.gameObject.TryGetComponent<Castle>(out Castle castle))
            {
                castle.TakeDamage(attack * 10);
                gameObject.SetActive(false);
            }
        }
    }
    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
    void GiveItems()
    {
        for(int i = 0; i < items.Length; i++)
        {
            for(int j = 0; j < itemsCount[i]; j++)
            {
                GameManager.Instance.inventory.AddItem(items[i]);
            }
        }
    }
}
