using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject bulletPrefab;
    public Vector2 moveDirection;
    float moveLimiter = 0.7f;
    bool up = false;

    //this is set in inspector
    public float runSpeed = 3.0f;

    // States
    public StateMachine SM;
    public MovingState moving;
    public DodgeState dodging;

    public override void Start ()
    {
        base.Start();
        SM = new StateMachine();
        moving = new MovingState(this, SM);
        dodging = new DodgeState(this, SM);
        SM.Initialize(moving);
    }

    public void fireBullet(Vector2 target)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().direction = target;
    }

    public void updateAnimation(float horizontal, float vertical)
    {
        //flip sprite
        if(!bodySprite.flipX && horizontal < 0.0) {
            bodySprite.flipX = true;
            shirtSprite.flipX = true;
            pantsSprite.flipX = true;
            hairSprite.flipX = true;
            handsSprite.flipX = true;
        }
        else if(bodySprite.flipX && horizontal > 0.0) {
            bodySprite.flipX = false;
            shirtSprite.flipX = false;
            pantsSprite.flipX = false;
            hairSprite.flipX = false;
            handsSprite.flipX = false;
        }
        //animations
        if(horizontal != 0.0 || vertical != 0.0) {
            animator.SetBool("move", true);
            if(vertical > 0.0 && !up) {
                up = true;
                handsSprite.sortingOrder = -1;
                animator.SetBool("up", up);
            }
            else if(vertical <= 0.0 && up) {
                up = false;
                handsSprite.sortingOrder = 1;
                animator.SetBool("up", up);
            }
        }
        else {
            animator.SetBool("move", false);
        }
        //syncSprites();
    }

    public void updateMovement(float h, float v, float speed=1.0f)
    {
        
        if (h != 0 && v != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            h *= moveLimiter;
            v *= moveLimiter;
        }

        float spd = runSpeed * speed;
        Vector2 velocity = new Vector2(h * spd, v * spd);
        //body.MovePosition(body.position + velocity);
        

        if(body.velocity != velocity)
        {
            body.velocity = velocity;
        }

        moveDirection = new Vector2(h, v);
    }

    public void triggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public bool isAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    public void Update()
    {
        syncSprites();
        SM.CurrentState.HandleInput();
        SM.CurrentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        SM.CurrentState.PhysicsUpdate();
    }
}
