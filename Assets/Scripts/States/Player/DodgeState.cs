using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : State
{
    private Vector2 direction;

    private float speed = 1.2f;

    public DodgeState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        direction = character.moveDirection;
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
        character.triggerAnimation("dodge");
    }

    public override void Exit()
    {
        base.Exit();
        character.triggerAnimation("reset");
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        if(character.isAnimationFinished())
        {
            stateMachine.ChangeState(character.states["moving"]);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        character.updateMovement(direction.x, direction.y, speed);
    }
}
