using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthBar;

    // Start is called before the first frame update
    public void Initialize(CharacterStats stats)
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = stats.maxHealth;
        healthBar.value = stats.maxHealth;
    }


    public void SetHealth(float hp)
    {
        healthBar.value = (int)hp;
    }
}
