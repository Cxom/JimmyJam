using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool canBePickedUp;
    public bool CanBePickedUp => canBePickedUp;

    [Header("Relative Transform When In Hand")]

    [SerializeField] public Vector3 offset = Vector3.zero;
    [SerializeField] public Vector3 rotation = Vector3.zero;
    [SerializeField] public Vector3 scale = new Vector3(1, 1, 1);

    private Vector3 originalScale;
    public Rigidbody Rigidbody { get; protected set; }

    protected void Start()
    {
        originalScale = transform.localScale;
        Rigidbody = GetComponent<Rigidbody>();
    }

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
    
    public virtual void SecondaryHold()
    {
        
    }
    
    public virtual void SecondaryUp()
    {
        
    }

    public void RestoreLocalScale()
    {
        transform.localScale = originalScale;
    }
    
    public void Toss(Transform playerHand)
    {
        
    }
}
