using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElectricBox : MonoBehaviour
{
    [SerializeField] private Interactable laserPuzzle;
    [SerializeField] private string laserPuzzleScene;
    
    [Header("")]
    [SerializeField] private Fountain fountain;
    
    [Header("")]
    [SerializeField] GameObject lockKey;
    [SerializeField] GameObject lockLock;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform hinge;
    [SerializeField] private float lockRotationTime = 1f;
    [SerializeField] private float doorRotationTime = 3f;
    [SerializeField] private float doorRotationStopAngle = -135f;
    
    private bool locked = true;
    
    internal void Unlock()
    {
        // TODO Remove this log
        Debug.Log("Unlocked electric box!");
        lockKey.SetActive(true);
        locked = false;
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        var stopTime = Time.time + lockRotationTime;
        var rotPerSec = -90 / lockRotationTime;
        var time = Time.time;
        while (time < stopTime)
        {
            yield return null;
            lockLock.transform.Rotate(lockLock.transform.forward, rotPerSec * (Time.time - time));
            time = Time.time;   
        }

        stopTime = Time.time + doorRotationTime;
        rotPerSec = doorRotationStopAngle / doorRotationTime;
        while (time < stopTime)
        {
            yield return null;
            door.transform.RotateAround(hinge.position, hinge.up, rotPerSec * (Time.time - time));
            time = Time.time;   
        }
        
        // TODO scene transition
        fountain.EnableElectricity();
    }
    
    public bool isLocked()
    {
        return locked;
    }
}
