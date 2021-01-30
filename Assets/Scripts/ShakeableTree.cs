using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeableTree : Interactable
{
    [SerializeField] private Interactable sticksPrefab;
    [SerializeField] private Transform[] placesToDropSticks;
    [SerializeField] private int timesToDropSticks = 1;
    
    private int timesDroppedSticks = 0;

    public override void PrimaryDown()
    {
        if (timesDroppedSticks < timesToDropSticks)
        {
            var placeToDropSticks = placesToDropSticks[Random.Range(0, placesToDropSticks.Length - 1)];
            var sticks = Instantiate(sticksPrefab, placeToDropSticks.position, Quaternion.Euler(0f, (float) Random.Range(0, 359), 0f));
            ++timesDroppedSticks;
        }
    }
}
