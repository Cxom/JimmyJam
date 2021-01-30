using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeafBlower : Interactable
{
    [SerializeField] private Transform blowerDirection;
    [SerializeField] private float airVelocity;
    [SerializeField] private float spawnRate = 0.2f;
    [SerializeField] private GameObject[] airParticles;
    
    private ParticleSystem _particleSystem;
    private float timeToSpawn;

    private new void Start()
    {
        base.Start();
     
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public override void PrimaryDown()
    {
        timeToSpawn = Time.time + spawnRate;
    }

    public override void PrimaryHold()
    {
        if (Time.time > timeToSpawn)
        {
            GameObject airParticlePrefab = airParticles[Random.Range(0, airParticles.Length - 1)];
            var airParticle = Instantiate(airParticlePrefab, blowerDirection.position, Random.rotationUniform);
            airParticle.GetComponent<Rigidbody>().AddForce(airVelocity * blowerDirection.forward);
            timeToSpawn += spawnRate;
        }
    }

    public override void PrimaryUp()
    {
        
    }
    
}
