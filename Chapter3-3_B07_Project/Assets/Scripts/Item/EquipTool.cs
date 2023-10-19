using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    public float attackDistance;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera _cam;
    private bool attacking;
    private bool isHit;

    public ParticleSystem swordEffect;
    private readonly int AnimAttack = Animator.StringToHash("Attack");

    protected virtual void Awake()
    {
        _cam = Camera.main;
        animator = GetComponent<Animator>();
    }
	private void Start()
	{
        swordEffect.Stop();
	}

	public override void OnAttackInput()
	{
		if(!attacking)
        {
            attacking = true;
            animator.SetTrigger(AnimAttack);
            Invoke(nameof(AttackDelay), attackRate);
            swordEffect.Play();
        }
	}
	private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
    }
	public void OnHit()
    {
		Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        isHit = Physics.Raycast(ray, out RaycastHit hit, attackDistance);
        if (isHit)
        {
			if (hit.collider.TryGetComponent(out Monster monster))
            {
				if (monster != null)
                {
                    Debug.Log(damage);
                    monster.TakeDamage(damage);
                    GameManager.Instance.garphicManager.MonsterHit(hit.point);
                }
            }
        }
    }


    private void AttackDelay()
    {
        attacking = false;
    }
}
