using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField]
    private MirrorGameManager mgm;

    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    // ID used for detection with the light sensor
    public int laserID;

    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private MeshRenderer pointerHead;
    [SerializeField]
    private LightBoxHit target;
    private Color col; //Color of the laser based on the provided laserID

    private void Start()
    {
        // Get Laser color of this component, reflect these changes on the laster pointer head
        col = mgm.laserColors[laserID];
        pointerHead.material.color = col;
    }

    /// <summary>
    /// Gizmo in the unity editor to allow us to see the ray's bouncing path
    /// </summary>
    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation * Quaternion.Euler(-90,0,0), 0.5f, EventType.Repaint);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, 0.25f);

        DrawPredictedReflectionPattern(this.transform.position, this.transform.up, maxReflectionCount);
    }

    /// <summary>
    /// Useful in the scene mode for setting up puzzles without needing to compile and run the game
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <param name="reflectionsRemaining"></param>
    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        if (reflectionsRemaining < 0)
        {
            return;
        }

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxStepDistance))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * maxStepDistance;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startingPosition, position);

        // hit a lightbox - Could be better to use object tags rather than string names.
        if (hit.collider.tag == "LightBox")
        {
            return;
        }

        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        //Collider hitObj = null;
        //bool hitSensor = false;
        int hits = 0;
        Vector3 direction = this.transform.up;
        Vector3 lastHitPosition = transform.position;

        // lineRenderer Colors
        //lineRenderer.SetColors(col, col);
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(col, 0.0f), new GradientColorKey(col, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;

        // Initial ray cast, as we bounce we add verticies and reflect!
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);

        while (hits <= maxReflectionCount)
        {
            if (Physics.Raycast(lastHitPosition, direction, out hit, maxStepDistance))
            {
                hits++;
                lineRenderer.positionCount = hits + 2;
                lineRenderer.SetPosition(hits, hit.point);

                direction = Vector3.Reflect(direction, hit.normal);
                lastHitPosition = hit.point;

                // hit a lightbox - Could be better to use object tags rather than string names.
                if (hit.collider.tag == "LightBox")
                {
                    hit.collider.gameObject.GetComponent<LightBoxHit>().FlipLight(laserID, true);
                    lineRenderer.SetPosition(hits + 1, lineRenderer.GetPosition(hits));
                    break;
                }
                else
                {
                    target.FlipLight(laserID, false);
                }
            }
            else {
                // Does not hit anything, restrict the max ray to reserve resources
                lastHitPosition += direction * maxStepDistance;
                break;
            }
        }
        
        lineRenderer.SetPosition(hits + 1, lastHitPosition);
    }
}
