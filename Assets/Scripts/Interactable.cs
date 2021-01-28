using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Relative Transform When In Hand")] 
    [SerializeField] public Vector3 offset;
    [SerializeField] public Vector3 rotation;
    [SerializeField] public Vector3 scale;

    public virtual void PrimaryDown()
    {
        
    }

    public virtual void PrimaryHold()
    {
        
    }

    public virtual void PrimaryUp()
    {
        
    }
    
    public virtual void SecondaryDown()
    {
        
    }
    
    public virtual  void SecondaryHold()
    {
        
    }
    
    public virtual void SecondaryUp()
    {
        
    }
    
}
