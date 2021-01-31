using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllenKey : Interactable
{
    [SerializeField] private WaterControlCabinet waterControlCabinet;
    [SerializeField] private Interactable allenKeySlot;

    public override void PrimaryDown()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, InteractController.LayerMask))
        {
            if (hit.transform.IsChildOf(allenKeySlot.transform) && hit.distance <= allenKeySlot.activationRange && !waterControlCabinet.isOn())
            {
                waterControlCabinet.TurnOn();
                InteractController.Instance().DeleteItem();
            }
        }
    }
}
