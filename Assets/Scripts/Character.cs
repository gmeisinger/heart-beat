using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Character : MonoBehaviour
{
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

    // Start is called before the first frame update
    public virtual void Start()
    {
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
