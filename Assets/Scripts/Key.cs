using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    [SerializeField] private ElectricBox electricBox;
    [SerializeField] private Interactable boxLock;

    public override void PrimaryDown()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, InteractController.LayerMask))
        {
            if (hit.transform.IsChildOf(boxLock.transform) && hit.distance <= boxLock.activationRange && electricBox.isLocked())
            {
                electricBox.Unlock();
                InteractController.Instance().DeleteItem();
            }
        }
    }
}
