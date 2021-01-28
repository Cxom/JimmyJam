using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private float reach = 100;
    
    private Interactable heldItem = null;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        var interactedWithItem = heldItem;
        if (!heldItem)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, reach))
            {
                // TODO use tags for this not recursive component searching
                Interactable interactable = hit.transform.GetComponentInParent<Interactable>();
                if (interactable)
                {
                    if (Input.GetButtonDown("Use_Primary") && interactable.CanBePickedUp)
                    {
                        PickUpItem(interactable);
                    } 
                    interactedWithItem = interactable;
                }
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
    
    private void PickUpItem(Interactable interactable)
    {
        heldItem = interactable;
        interactable.transform.SetParent(playerHand);
        interactable.transform.position = interactable.offset;
        interactable.transform.rotation = Quaternion.Euler(interactable.rotation);
        interactable.transform.localScale = interactable.scale;
    }
}
