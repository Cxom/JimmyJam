using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Interactable
{

    [SerializeField] private GameObject rungs;
    [SerializeField] private Collider ladderCollider;
    
    internal void AddRungs()
    {
        rungs.SetActive(true);
        ladderCollider.enabled = true;
    }


    public bool HasRungs()
    {
        return rungs.activeSelf;
    }
}
