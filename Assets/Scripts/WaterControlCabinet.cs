using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControlCabinet : MonoBehaviour
{
    [SerializeField] private Fountain fountain;
    
    [Header("")]
    [SerializeField] private Interactable allenKeySlot;
    [SerializeField] private GameObject door;
    [SerializeField] private Transform hinge;
    [SerializeField] private float doorRotationTime = 2f;
    [SerializeField] private float doorRotationStopAngle = -135f;

    [Header("Animation Variables")]
    [SerializeField] private GameObject allenKeyInSlot;
    [SerializeField] private float allenKeyRotationTime = 2f;
    [Tooltip("Should be a multiple of 60")]
    [SerializeField] private float allenKeyRotationStopAngle = -120f;
    
    private bool open = false;
    private bool on = false;

    internal void OpenOrClose()
    {
        StartCoroutine(OpenOrCloseDoor());
        open = !open;
    }

    private IEnumerator OpenOrCloseDoor()
    {
        var rotationAngle = (open ? -1 : 1) * doorRotationStopAngle;
        var stopTime = Time.time + doorRotationTime;
        var rotPerSec = rotationAngle / doorRotationTime;
        var time = Time.time;
        while (time < stopTime)
        {
            yield return null;
            door.transform.RotateAround(hinge.position, hinge.up, rotPerSec * (Time.time - time));
            time = Time.time;
        }
    }

    public bool isOn()
    {
        return on;
    }

    public void TurnOn()
    {
        // TODO Turn On
        Debug.Log("Water control cabinet turned on");
        allenKeyInSlot.SetActive(true);
        // allenKeySlot.GetComponent<Interactable>().enabled = false;
        StartCoroutine(TurnOnAnimation());
    }

    private IEnumerator TurnOnAnimation()
    {
        var stopTime = Time.time + allenKeyRotationTime;
        var rotPerSec = allenKeyRotationStopAngle / allenKeyRotationTime;
        var time = Time.time;
        while (time < stopTime)
        {
            yield return null;
            allenKeyInSlot.transform.Rotate(allenKeySlot.transform.forward, rotPerSec * (Time.time - time));
            time = Time.time;
        }

        on = true;
        fountain.EnableWater();
    }
}
