using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorGameManager : MonoBehaviour
{
    // Collection of colors shared by our scene.
    // Manually set colors with colorID
    public List<Color> laserColors = new List<Color>() { Color.red, Color.cyan, Color.green, Color.yellow, Color.white };

    private List<bool> lasorHits = new List<bool>() { false, false, false, false, false };

    // Game Logic
    public void updateOn(int lightID, bool state)
    {
        //Debug.Log("Num Activated Lasors --> " + lasorHits.ToString());

        lasorHits[lightID] = state;

        if (lasorHits.TrueForAll(x => x))
        {
            Debug.Log("End MiniGame!!!");
        }
    }
}
