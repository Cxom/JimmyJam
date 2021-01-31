using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksForRungs : Interactable
{
    private Ladder highlightedLadder;
    
    public void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, InteractController.LayerMask))
        {
            Ladder ladder = hit.transform.GetComponentInParent<Ladder>();
            if (ladder && hit.distance <= ladder.activationRange && !ladder.HasRungs())
            {
                if (ladder != highlightedLadder)
                {
                    if (highlightedLadder)
                    {
                        highlightedLadder.Outline.enabled = false;
                    }
                    highlightedLadder = ladder;
                    highlightedLadder.Outline.enabled = true;
                }
                return;
            }
        }

        if (highlightedLadder)
        {
            highlightedLadder.Outline.enabled = false;
            highlightedLadder = null;
        }
    }

    public override void PrimaryDown()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 10, InteractController.LayerMask))
        {
            Ladder ladder = hit.transform.GetComponentInParent<Ladder>();
            if (ladder && hit.distance <= ladder.activationRange)
            {
                ladder.AddRungs();
                InteractController.Instance().DeleteItem();
            }
        }
    }
}
