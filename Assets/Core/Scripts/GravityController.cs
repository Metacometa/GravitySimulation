using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    private List<GravityBody2D> gravityBodies;

    private void Awake()
    {
        gravityBodies = FindObjectsByType<GravityBody2D>(FindObjectsSortMode.None).ToList();
    }

    private void FixedUpdate()
    {
        foreach (var gravityBody in gravityBodies)
            gravityBody.ResetForce();

        for (int i = 0; i < gravityBodies.Count; ++i)
        {
            for (int j = i + 1; j < gravityBodies.Count; ++j)
            {
                if (gravityBodies[i].gameObject.activeInHierarchy == false
                    || gravityBodies[j].gameObject.activeInHierarchy == false)
                    return;

                gravityBodies[i].AddForce(gravityBodies[j]);
                gravityBodies[j].AddForce(gravityBodies[i]);
            }
        }

        foreach (var gravityBody in gravityBodies)
            gravityBody.ApplyForce();
    }
}
