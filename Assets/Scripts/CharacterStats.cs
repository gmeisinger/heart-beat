using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;

    public float curEnergy = 0;
    public float maxEnergy = 100;

    private float energyPerSecond = 25.0f;

    public HealthBar healthBar;
    public EnergyBar energyBar;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        curEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        curEnergy += energyPerSecond * Time.deltaTime;
        if(curEnergy > maxEnergy) {
            curEnergy = maxEnergy;
        }
        healthBar.SetHealth(curHealth);
        energyBar.SetEnergy(curEnergy);
    }

    public void takeDamage( int damage )
    {
        curHealth -= damage;
    }

    public void loseEnergy( float cost )
    {
        curEnergy -= cost;
    }
}
