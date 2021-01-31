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

    private static InteractController Inst;
    public static InteractController Instance()
    {
        return Inst;
    }

    private static LayerMask layerMask;
    public static LayerMask LayerMask => layerMask;

    void Awake()
    {
        Inst = this;
        layerMask = ~LayerMask.GetMask("Player", "InteractableIgnore");
    }

    void Update()
    {
        if (heldItem && Input.GetButtonDown("Drop"))
        {
            DropItem();
        }

        var interactedWithItem = heldItem;
        // if (!heldItem)
        // {
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
                    
                    if (!heldItem)
                    {
                        interactedWithItem = interactable;
                    }
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
        // }

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
        highlighted.Outline.enabled = true;
    }

    private void ClearHighlighted()
    {
        if (!highlighted) { return; }
        highlighted.Outline.enabled = false;
        highlighted = null;
    }

    

    private void PickUpItem(Interactable interactable)
    {
        if (heldItem)
        {
            DropItem();
        }
        heldItem = interactable;
        ClearHighlighted();
        interactable.Rigidbody.isKinematic = true;
        interactable.transform.SetParent(playerHand);
        interactable.transform.localPosition = interactable.offset;
        interactable.transform.localRotation = Quaternion.Euler(interactable.rotation);
        interactable.transform.localScale = interactable.scale;
    }

    internal void DropItem()
    {
        heldItem.transform.SetParent(droppedItemsParent);
        heldItem.Rigidbody.isKinematic = false;
        heldItem.RestoreLocalScale();
        heldItem.Toss(playerHand);
        heldItem = null;
    }
    
    internal void DeleteItem()
    {
        Destroy(heldItem.gameObject);
        heldItem = null;
    }
}
