using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;

    // Start is called before the first frame update
    public void Initialize(CharacterStats stats)
    {
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = stats.maxEnergy;
        energyBar.value = stats.maxEnergy;
    }

    public void SetEnergy(float en)
    {
        energyBar.value = (int)en;
    }
}
