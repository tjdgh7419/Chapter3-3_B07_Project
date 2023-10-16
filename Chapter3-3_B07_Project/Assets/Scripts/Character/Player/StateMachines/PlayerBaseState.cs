using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        groundData = stateMachine.Player.Data.GroundedData;
    }
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled += OnMoveCanceled;
        input.PlayerActions.Run.started += OnRunStarted;

        stateMachine.Player.Input.PlayerActions.Attack.performed += OnAttackPerformed;
        stateMachine.Player.Input.PlayerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = stateMachine.Player.Input;
        input.PlayerActions.Move.canceled -= OnMoveCanceled;
        input.PlayerActions.Run.started -= OnRunStarted;

        stateMachine.Player.Input.PlayerActions.Attack.performed -= OnAttackPerformed;
        stateMachine.Player.Input.PlayerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnRunStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }
    protected virtual void OnAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnAttackCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;

    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PlayerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Rotate(movementDirection);
        Move(movementDirection);
    }

    protected void ForceMove()
    {
        //stateMachine.Player.Rigidbody.AddForce(stateMachine.Player.ForceReceiver.Movement * Time.deltaTime, ForceMode.Impulse);
        stateMachine.Player.Rigidbody.AddForce(stateMachine.Player.transform.forward.normalized, ForceMode.Impulse);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }
    private void Move(Vector3 movementDirection)
    {
        
        float movementSpeed = GetMovementSpeed();
        /*stateMachine.Player.Rigidbody.MovePosition(
            stateMachine.Player.transform.position + ((movementDirection * movementSpeed) + stateMachine.Player.ForceReceiver.Movement) * Time.fixedDeltaTime
            );*/
        stateMachine.Player.Rigidbody.MovePosition(
            stateMachine.Player.transform.position + (movementDirection * movementSpeed) * Time.fixedDeltaTime
            );
    }


    private void Rotate(Vector3 movementDirection)
    {
        if(movementDirection != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);

            //stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }
    }
    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }    
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetCurrentAnimatorStateInfo(0);

        if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        } else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        } else
        {
            return 0f;
        }

    }
    
}
