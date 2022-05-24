using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	public EntityData data;
	public Vector2 moveDirection;
    protected float moveLimiter = 0.7f;
    protected bool up = false;

    //this is set in inspector
    public float runSpeed = 150.0f;

    // States
    public StateMachine SM;
	public Dictionary<string, State> states = new Dictionary<string, State>();

	// animation vars
    protected Rigidbody2D body;
    protected SpriteRenderer bodySprite;
	protected Animator animator;
	public Color skinColor;

	protected virtual void Update()
	{
		SM.CurrentState.HandleInput();
        SM.CurrentState.LogicUpdate();
	}

	protected virtual void FixedUpdate()
	{
		SM.CurrentState.PhysicsUpdate();
	}

	public virtual void updateAnimation(float horizontal, float vertical)
	{
		// //animations
        // if(horizontal != 0.0 || vertical != 0.0) {
        //     animator.SetBool("move", true);
        // }
        // else {
        //     animator.SetBool("move", false);
        // }
	}

	public virtual void setShaderBool(string property, bool value)
	{
		bodySprite.material.SetInt(property, value ? 1 : 0);
	}

	public virtual void setShaderFloat(string property, float value)
	{
		bodySprite.material.SetFloat(property, value);
	}

	public virtual void Start()
	{
		SM = new StateMachine();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodySprite = GetComponent<SpriteRenderer>();
        bodySprite.color = skinColor;
		bodySprite.material = Instantiate<Material>(bodySprite.material);
		// if(data != null)
		// {
		// 	foreach(var stateData in data.startingStates)
		// 	{
		// 		Type stateType = stateData.stateType.GetClass();
		// 		if(stateType.IsAssignableFrom(typeof(State)))
		// 		{
		// 			states.Add(stateData.key, Activator.CreateInstance(stateType))
		// 		}
		// 	}
		// }
	}

	public void triggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public bool isAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
