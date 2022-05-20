using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public CharacterStats playerStats;

    

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = playerStats.maxEnergy;
        energyBar.value = playerStats.maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEnergy(float en)
    {
        energyBar.value = (int)en;
    }
}
