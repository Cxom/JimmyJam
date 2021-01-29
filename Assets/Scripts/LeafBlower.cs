using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBlower : Interactable
{

    private ParticleSystem _particleSystem;
    
    private new void Start()
    {
        base.Start();
     
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public override void PrimaryDown()
    {
        _particleSystem.Play();
    }

    public override void PrimaryUp()
    {
        _particleSystem.Stop();
    }
    
}
