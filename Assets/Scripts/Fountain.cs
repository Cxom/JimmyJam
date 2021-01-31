using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{

    // [SerializeField] private SoccerBall soccerBall;
    [SerializeField] private GameObject flowingWater;

    private bool electricityOn = false;
    private bool waterOn = false;
    private bool fountainOn = false;
    
    internal void EnableElectricity()
    {
        electricityOn = true;
        TryTurnOn();
    }
    
    internal void EnableWater()
    {
        waterOn = true;
        TryTurnOn();
    }
    
    private void TryTurnOn()
    {
        if (electricityOn && waterOn && !fountainOn)
        {
            TurnOn();
        }
    }
    
    private void TurnOn()
    {
        fountainOn = true;
        flowingWater.SetActive(true);
        // soccerBall.LaunchFromFountain();
    }
}
