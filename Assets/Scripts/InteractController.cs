using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private Interactable heldItem = null;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (heldItem)
        {
            // Primary
            if (Input.GetButtonDown("Use_Primary"))
            {
                heldItem.PrimaryDown();
            }
            if (Input.GetButton("Use_Primary"))
            {
                heldItem.PrimaryHold();
            }
            if (Input.GetButtonUp("Use_Primary"))
            {
                heldItem.PrimaryUp();
            }
            // Secondary
            if (Input.GetButtonDown("Use_Secondary"))
            {
                heldItem.SecondaryDown();
            }
            if (Input.GetButton("Use_Secondary"))
            {
                heldItem.SecondaryHold();
            }
            if (Input.GetButtonUp("Use_Secondary"))
            {
                heldItem.SecondaryUp();
            }
        }

    }
}
