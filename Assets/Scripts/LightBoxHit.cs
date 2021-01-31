using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoxHit : MonoBehaviour
{
    [SerializeField]
    private MirrorGameManager mgm;

    public int lightID = 0;

    [SerializeField]
    private GameObject responseLight;

    private void Start()
    {
        this.GetComponent<Renderer>().material.color = mgm.laserColors[lightID];
    }

    public void FlipLight(int hitterID, bool hit)
    {
        if (hitterID == lightID && hit)
        {
            mgm.updateOn(lightID, true);
            responseLight.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            mgm.updateOn(lightID, false);
            responseLight.GetComponent<Renderer>().material.color = Color.grey;
        }
    }
}
