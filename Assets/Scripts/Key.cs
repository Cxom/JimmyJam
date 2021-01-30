using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{

    [SerializeField] private ElectricBox electricBox;
    [SerializeField] private Interactable boxLock;

    public void Update()
    {
        LayerMask layerMask = LayerMask.NameToLayer("Player");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, layerMask))
        {
            if (hit.transform.IsChildOf(boxLock.transform) && hit.distance <= boxLock.activationRange && electricBox.isLocked())
            {
                boxLock.Outline.enabled = true;
                return;
            }
        }

        boxLock.Outline.enabled = false;
    }

    public override void PrimaryDown()
    {
        LayerMask layerMask = LayerMask.NameToLayer("Player");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, layerMask))
        {
            if (hit.transform.IsChildOf(boxLock.transform) && hit.distance <= boxLock.activationRange && electricBox.isLocked())
            {
                electricBox.Unlock();
                InteractController.Instance().DeleteItem();
            }
        }
    }
}
