using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeableTree : Interactable
{
    public override void PrimaryDown()
    {
        Debug.Log($"Shook a Tree: {transform.name}");
    }
}
