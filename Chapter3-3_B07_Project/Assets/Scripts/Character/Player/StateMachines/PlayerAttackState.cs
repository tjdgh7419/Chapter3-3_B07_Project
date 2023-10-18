using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    AttackInfoData attackInfoData;
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    public override void Enter()
    {

        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        int attackIndex = stateMachine.AttackIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(attackIndex);
        stateMachine.Player.Animator.SetInteger("AttackIndex", attackIndex);
    }

    public override void Exit()
    {


        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
        stateMachine.AttackIndex = 0;
    }

    private void TryApplyForce()
    {
        /*stateMachine.Player.ForceReceiver.Reset();
        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);*/
        stateMachine.Player.Rigidbody.AddForce(stateMachine.Player.transform.forward.normalized * attackInfoData.Force, ForceMode.Impulse);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if(normalizedTime < 1f)
        {
            TryApplyForce();
        }
        else
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
