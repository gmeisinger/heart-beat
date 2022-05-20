using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private bool selected = false;

    public bool Selected
    {
        get { return selected; }
        set 
        { 
            selected = value;
            if (value)
            {
                sprite.material.SetFloat("_OutlineThickness", 2f);
            }
            else
            {
                sprite.material.SetFloat("_OutlineThickness", 0f);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if(!selected)
        {
            sprite.material.SetFloat("_OutlineThickness", 1f);
        }
        
    }

    private void OnMouseExit()
    {
        if (!selected)
        {
            sprite.material.SetFloat("_OutlineThickness", 0f);
        }
    }

    private void OnMouseUp()
    {
        Selected = !Selected;
    }
}
