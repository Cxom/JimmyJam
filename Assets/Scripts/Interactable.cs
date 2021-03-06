using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool canBePickedUp;
    public bool CanBePickedUp => canBePickedUp;
    [SerializeField] public float activationRange = 5f;
    
    [Header("Relative Transform When In Hand")]

    [SerializeField] public Vector3 offset = Vector3.zero;
    [SerializeField] public Vector3 rotation = Vector3.zero;
    [SerializeField] public Vector3 scale = new Vector3(1, 1, 1);

    private Vector3 originalScale;
    public Rigidbody Rigidbody { get; protected set; }
    public Outline Outline { get; private set; }

    protected void Start()
    {
        originalScale = transform.localScale;
        Rigidbody = GetComponent<Rigidbody>();
        Outline = GetComponent<Outline>();
        Outline.enabled = false;
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
