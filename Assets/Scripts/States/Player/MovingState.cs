using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    private float horizontalInput;
    private float verticalInput;

    private bool dodge;
    private bool shoot;

    private CharacterStats stats;

    public MovingState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
        stats = player.GetComponent<CharacterStats>();
    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = verticalInput = 0.0f;
    }

    public override void Exit()
    {
        base.Exit();
        horizontalInput = verticalInput = 0.0f;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        player.updateAnimation(horizontalInput, verticalInput);

        dodge = Input.GetButtonDown("Jump");
        shoot = false;//Input.GetButtonDown("Fire1");
    }

    public override void LogicUpdate()
    {
        if(dodge)
        {
            if(stats.curEnergy >= 50.0f) {
                stats.loseEnergy(50.0f);
                stateMachine.ChangeState(player.dodging);
            }
        }
        else if(shoot)
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
            player.fireBullet(target);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.updateMovement(horizontalInput, verticalInput);
    }

}
