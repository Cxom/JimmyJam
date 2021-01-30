using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeafBlower : Interactable
{
    [SerializeField] private Transform blowerDirection;
    [SerializeField] private float airVelocity;
    [SerializeField] private GameObject[] airParticles;
    
    private ParticleSystem _particleSystem;
    private bool blowing;

    private new void Start()
    {
        base.Start();
     
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public override void PrimaryDown()
    {
        blowing = true;
    }

    public override void PrimaryHold()
    {
        GameObject airParticle = airParticles[Random.Range(0, airParticles.Length - 1)];
        Instantiate(airParticle, blowerDirection.position, Random.rotationUniform);
        airParticle.GetComponent<Rigidbody>().velocity = airVelocity * blowerDirection.forward;
    }

    public override void PrimaryUp()
    {
        blowing = false;
    }
}
