using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    public GameObject energyLow;
    public GameObject energyHigh;
    public GameObject energyFull;

    void ChangeEnergySprite(int energy) 
    {
        if (energy == 0)
        {
        energyLow.SetActive(true);
        energyHigh.SetActive(false);
        energyFull.SetActive(false);
        }

        if (energy == 1)
        {
            energyLow.SetActive(false);
            energyHigh.SetActive(false);
            energyFull.SetActive(true);
        }

        if (energy == 2) 
        {
            energyLow.SetActive(false);
            energyHigh.SetActive(false);
            energyFull.SetActive(true);
        }
    }
}

