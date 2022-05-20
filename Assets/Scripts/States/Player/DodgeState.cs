using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    private Vector2 direction;

    private float speed = 1.2f;

    public DodgeState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        direction = player.moveDirection;
        if(direction.x > 0) {
            direction.x = 1;
        }
        else if(direction.x < 0) {
            direction.x = -1;
        }
        if(direction.y > 0) {
            direction.y = 1;
        }
        else if(direction.y < 0) {
            direction.y = -1;
        }
        player.triggerAnimation("dodge");
    }

    public override void Exit()
    {
        base.Exit();
        player.triggerAnimation("reset");
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        if(player.isAnimationFinished())
        {
            stateMachine.ChangeState(player.moving);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.updateMovement(direction.x, direction.y, speed);
    }
}
