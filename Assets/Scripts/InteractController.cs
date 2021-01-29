using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform droppedItemsParent;
    [SerializeField] private float reach = 100;
    
    private Interactable heldItem = null;
    private Interactable highlighted = null;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (heldItem && Input.GetButtonDown("Drop"))
        {
            DropItem();
        }

        var interactedWithItem = heldItem;
        if (!heldItem)
        {
            LayerMask layerMask = LayerMask.NameToLayer("Player");
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(ray, out RaycastHit hit, reach, layerMask))
            {
                // TODO use tags for this not recursive component searching
                Interactable interactable = hit.transform.GetComponentInParent<Interactable>();
                if (interactable && hit.distance <= interactable.activationRange)
                {
                    if (interactable != highlighted)
                    {
                        ClearHighlighted();
                        SetHighlighted(interactable);
                    }
                    
                    if (Input.GetButtonDown("Use_Primary") && interactable.CanBePickedUp)
                    {
                        PickUpItem(interactable);
                    } 
                    interactedWithItem = interactable;
                }
                else
                {
                    // TODO refactor whole update method
                    ClearHighlighted();
                }
            } else if (highlighted)
            {
                ClearHighlighted();
            }
        }

        if (interactedWithItem)
        {
            // Primary
            if (Input.GetButtonDown("Use_Primary"))
            {
                interactedWithItem.PrimaryDown();
            }
            if (Input.GetButton("Use_Primary"))
            {
                interactedWithItem.PrimaryHold();
            }
            if (Input.GetButtonUp("Use_Primary"))
            {
                interactedWithItem.PrimaryUp();
            }
            // Secondary
            if (Input.GetButtonDown("Use_Secondary"))
            {
                interactedWithItem.SecondaryDown();
            }
            if (Input.GetButton("Use_Secondary"))
            {
                interactedWithItem.SecondaryHold();
            }
            if (Input.GetButtonUp("Use_Secondary"))
            {
                interactedWithItem.SecondaryUp();
            }
        }

    }

    private void SetHighlighted(Interactable interactable)
    {
        highlighted = interactable;
        highlighted.outline.enabled = true;
    }

    private void ClearHighlighted()
    {
        if (!highlighted) { return; }
        highlighted.outline.enabled = false;
        highlighted = null;
    }

    private void DropItem()
    {
        heldItem.transform.SetParent(droppedItemsParent);
        heldItem.Rigidbody.isKinematic = false;
        heldItem.RestoreLocalScale();
        heldItem.Toss(playerHand);
        heldItem = null;
    }

    private void PickUpItem(Interactable interactable)
    {
        heldItem = interactable;
        interactable.Rigidbody.isKinematic = true;
        interactable.transform.SetParent(playerHand);
        interactable.transform.localPosition = interactable.offset;
        interactable.transform.localRotation = Quaternion.Euler(interactable.rotation);
        interactable.transform.localScale = interactable.scale;
    }
}
