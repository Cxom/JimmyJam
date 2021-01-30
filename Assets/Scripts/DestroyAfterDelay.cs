using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float lifetime;
    
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
