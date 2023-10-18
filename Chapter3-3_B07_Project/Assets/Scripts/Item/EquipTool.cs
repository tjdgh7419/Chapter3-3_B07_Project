using System.Collections;
using System.Collections.Generic;
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

    private readonly int AnimAttack = Animator.StringToHash("Attack");

    protected virtual void Awake()
    {
        _cam = Camera.main;
        animator = GetComponent<Animator>();
    }

	public override void OnAttackInput()
	{
		if(!attacking)
        {
            attacking = true;
            animator.SetTrigger(AnimAttack);
            Invoke(nameof(AttackDelay), attackRate);
        }
	}

    private void AttackDelay()
    {
        attacking = false;
    }
}
