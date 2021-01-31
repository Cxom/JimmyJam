using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControlCabinetDoor : Interactable
{
    [SerializeField] private WaterControlCabinet waterControlCabinet;

    public override void PrimaryDown()
    {
        waterControlCabinet.OpenOrClose();
    }
    
}
