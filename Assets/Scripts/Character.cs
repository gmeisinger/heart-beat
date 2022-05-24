using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : Entity
{
	
    protected SpriteRenderer shirtSprite;
    protected SpriteRenderer pantsSprite;
    protected SpriteRenderer hairSprite;
    protected SpriteRenderer handsSprite;
    
    protected Animator shirtAnimator;
    protected Animator pantsAnimator;
    protected Animator hairAnimator;
    protected Animator handsAnimator;

    protected SpriteRenderer[] renderers;
    protected Dictionary<string, Sprite[]> sprites;// = new Dictionary<string, Sprite[]>();

    
    public Color shirtColor;
    public Color pantsColor;
    public Color hairColor;

	public virtual void updateMovement(float h, float v, float speed=1.0f)
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

    // Start is called before the first frame update
    public override void Start()
    {
		base.Start();
        // layers
        shirtSprite = transform.Find("Shirt").GetComponent<SpriteRenderer>();
        shirtSprite.color = shirtColor;
        pantsSprite = transform.Find("Pants").GetComponent<SpriteRenderer>();
        pantsSprite.color = pantsColor;
        hairSprite = transform.Find("Hair").GetComponent<SpriteRenderer>();
        hairSprite.color = hairColor;
        handsSprite = transform.Find("Hands").GetComponent<SpriteRenderer>();
        handsSprite.color = skinColor;
		shirtSprite.material = Instantiate<Material>(bodySprite.material);
		pantsSprite.material = Instantiate<Material>(bodySprite.material);
		hairSprite.material = Instantiate<Material>(bodySprite.material);
		handsSprite.material = Instantiate<Material>(bodySprite.material);
        // hands still has an animator
        handsAnimator = transform.Find("Hands").GetComponent<Animator>();
        renderers = new SpriteRenderer[] {shirtSprite, pantsSprite, hairSprite, handsSprite};
        sprites = loadSprites();
        Debug.Log("Character start.");
    }

	protected override void Update()
    {
        syncSprites();
        base.Update();
    }

	public override void updateAnimation(float horizontal, float vertical)
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

	public override void setShaderBool(string property, bool value)
	{
		bodySprite.material.SetInt(property, value ? 1 : 0);
		shirtSprite.material.SetInt(property, value ? 1 : 0);
		pantsSprite.material.SetInt(property, value ? 1 : 0);
		hairSprite.material.SetInt(property, value ? 1 : 0);
		handsSprite.material.SetInt(property, value ? 1 : 0);
	}

	public override void setShaderFloat(string property, float value)
	{
		bodySprite.material.SetFloat(property, value);
		shirtSprite.material.SetFloat(property, value);
		pantsSprite.material.SetFloat(property, value);
		hairSprite.material.SetFloat(property, value);
		handsSprite.material.SetFloat(property, value);
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
