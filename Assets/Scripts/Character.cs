using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Character : Entity
{
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
    protected SpriteRenderer shirtSprite;
    protected SpriteRenderer pantsSprite;
    protected SpriteRenderer hairSprite;
    protected SpriteRenderer handsSprite;
    protected Animator animator;
    protected Animator shirtAnimator;
    protected Animator pantsAnimator;
    protected Animator hairAnimator;
    protected Animator handsAnimator;

    protected SpriteRenderer[] renderers;
    protected Dictionary<string, Sprite[]> sprites;// = new Dictionary<string, Sprite[]>();

    public Color skinColor;
    public Color shirtColor;
    public Color pantsColor;
    public Color hairColor;

	public abstract void updateMovement(float h, float v);
	public abstract void updateMovement(float h, float v, float speed);

    // Start is called before the first frame update
    public virtual void Start()
    {
		SM = new StateMachine();
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodySprite = GetComponent<SpriteRenderer>();
        bodySprite.color = skinColor;
        // layers
        shirtSprite = transform.Find("Shirt").GetComponent<SpriteRenderer>();
        shirtSprite.color = shirtColor;
        pantsSprite = transform.Find("Pants").GetComponent<SpriteRenderer>();
        pantsSprite.color = pantsColor;
        hairSprite = transform.Find("Hair").GetComponent<SpriteRenderer>();
        hairSprite.color = hairColor;
        handsSprite = transform.Find("Hands").GetComponent<SpriteRenderer>();
        handsSprite.color = skinColor;
        // hands still has an animator
        handsAnimator = transform.Find("Hands").GetComponent<Animator>();
        renderers = new SpriteRenderer[] {shirtSprite, pantsSprite, hairSprite, handsSprite};
        sprites = loadSprites();
        Debug.Log("Character start.");
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

	public void triggerAnimation(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    public bool isAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }

    // load sprites from resources
    public Dictionary<string, Sprite[]> loadSprites()
    {
        Dictionary<string, Sprite[]> spritesDict = new Dictionary<string, Sprite[]>();
        //Debug.Log(spritesDict);
        foreach (SpriteRenderer s in renderers)
        {
            string baseName = s.sprite.name.Split('_')[0];
            Sprite[] sprites = Resources.LoadAll<Sprite>(baseName);
            spritesDict.Add(baseName, sprites);
            //Debug.Log(sprites.Length);
        }
        return spritesDict;
    }
 
    // sync sprite frames
    public void syncSprites()
    {
        foreach (SpriteRenderer s in renderers)
        {
            string baseName = s.sprite.name.Split('_')[0];
            int index = Int32.Parse(bodySprite.sprite.name.Split('_')[1]);
            
            Sprite newSprite = this.sprites[baseName][index];
            s.sprite = newSprite;
            //Debug.Log(baseName + tail);
        }
    }

	
}
