using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBox : MonoBehaviour
{
    [SerializeField] GameObject lockKey;
    
    private bool locked = true;
    
    internal void Unlock()
    {
        // TODO Open door
        Debug.Log("Unlocked electric box!");
        lockKey.SetActive(true);
        locked = false;
    }
    
    public bool isLocked()
    {
        return locked;
    }
}
