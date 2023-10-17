using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public enum NPCAIState
{
    Idle,
    Interact,
    Walk
}
public class NPC : MonoBehaviour
{
    [Header("AI")]
    public NPCAIState npcAI;
    public float detectDistance;

    public NpcSO npcSO;
    protected float playerDistance;
    public float fieldOfView = 120f;
    private bool canTalk;
    [SerializeField] private GameObject talkPos, NamePos, buttonPos, Canvas;
    [SerializeField] protected GameObject player, button;
    GameObject image = null;
    [SerializeField] protected TextMeshProUGUI talk, _name;
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        npcSO.npcType = 0;
        npcAI = NPCAIState.Idle;
        talk.transform.position = talkPos.transform.position;
        talk.gameObject.SetActive(false);
        _name.transform.position = NamePos.transform.position;
        button.transform.position = buttonPos.transform.position;
        button.gameObject.SetActive(false);
        _name.text = npcSO.npcName;
    }
    protected virtual void Update()
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
        }
    }
    protected void PassiveUpdate()
    {
        if(npcAI != NPCAIState.Interact && playerDistance < detectDistance)
        {
            if (IsPlayerInFieldOfView())
            {
                canTalk = true;
                npcAI = NPCAIState.Interact;
            }
        }
        else if(npcSO.npcType == 1 && npcAI != NPCAIState.Walk)
        {
            npcAI = NPCAIState.Walk;
        }
    }
    protected void InteractUpdate()
    {
        if (!npcSO.interact)
        {
            if (canTalk)
            {
                InvokeRepeating("NpcTalk", 0.1f, 2.5f);
            }
            canTalk = false;
        }
    }
    protected bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }
    void NpcTalk()
    {
        talk.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        int rand = Random.Range(0, 2);
        talk.text = npcSO.npcLine[rand].ToString();
    }
    public void StartInteract()
    {
        if(image == null)
        {
            npcSO.interact = true;
            image = Instantiate<GameObject>(npcSO.ui);
            image.transform.SetParent(Canvas.transform, false);
            CancelInvoke("NpcTalk");
        }
        image.SetActive(true);
    }
    public void Byebutton()
    {
        npcSO.interact = false;
        image.SetActive(false);
        talk.text = npcSO.npcLine[2].ToString();
        StartCoroutine("CancelInteract");
    }
    IEnumerator CancelInteract()
    {
        npcAI = NPCAIState.Idle;
        fieldOfView = 0;
        yield return new WaitForSeconds(3f);
        talk.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        fieldOfView = 120f;
    }
}
