using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Behavior : MonoBehaviour
{
    public Task3Manager myManager;
    private Vector3 position;
    private Vector3 newPosition;
    private Vector3 velocity;

    private GameObject[] allParticles;
    private Vector3 distance;
    private float m = 3.0f;

    private float deltatime = 0.000001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = FindNewSpeed(velocity);
        Debug.Log("velocity" + velocity);
        position = this.transform.position;
        Debug.Log("position" + position);
        newPosition = position + velocity;
        (newPosition, velocity) = CheckForCollision(position, newPosition, velocity);

        // Updating particle position
        this.transform.position = newPosition;
    }

    public Vector3 FindNewSpeed(Vector3 velcty)
    {
        allParticles = myManager.allParticles;
        Vector3 acceleration = Vector3.zero;
        float nDistance;

        foreach (GameObject particle in allParticles)
        {
            if (particle != this.gameObject)
            {
                nDistance = Vector3.Distance(particle.transform.position, this.transform.position);

                acceleration += (this.transform.position - particle.transform.position) / Mathf.Pow(nDistance, 3.0f);

                acceleration *= -myManager.repulseWeight / m;
            }
        }

        // Velocity update
        velcty += (acceleration * deltatime);
        return velcty;
    }


    public (Vector3, Vector3) CheckForCollision(Vector3 pos, Vector3 newPos, Vector3 velcty)
    {
        float sphereRadius = 5.0f;
        Vector3 intersectVector;
        Vector3 intersectPoint;
        float a;
        float b;
        float c;
        float t1;
        float t2;
        Vector3 choice1;
        Vector3 choice2;
        Vector3 v;
        float dist;
        Vector3 nVector;
        Vector3 newDirection;


        if (newPos.magnitude > sphereRadius)
        {
            // Find direction vector between current position and new position
            intersectVector = newPos - pos;

            a = intersectVector.magnitude;
            b = -2 * Vector3.Dot(intersectVector, (-1 * pos));
            c = newPos.magnitude - Mathf.Pow(sphereRadius, 2.0f);

            t1 = (-b + Mathf.Pow(b * b - 4 * a * c, -0.5f)) / (2 * a);
            t2 = (-b - Mathf.Pow(b * b - 4 * a * c, -0.5f)) / (2 * a);

            // Define two posible intersection points
            choice1 = pos + t1 * intersectVector;
            choice2 = pos + t2 * intersectVector;


            // Choose the correct one
            if ((choice1.x < newPos.x) && (choice1.x > pos.x))
            {
                if ((choice1.y < newPos.y) && (choice1.y > pos.y))
                {
                    if ((choice1.z < newPos.z) && (choice1.z > pos.z))
                    {
                        intersectPoint = choice1;
                    }
                    else
                    {
                        intersectPoint = choice2;
                    }
                }
                else
                {
                    intersectPoint = choice2;
                }
            }
            else
            {
                intersectPoint = choice2;
            }

            // Calculate plane creds
            nVector = intersectPoint.normalized; // Becuse sphere center is (0,0,0)

            // Find symetric to plane point (new position)
            v = newPos - intersectPoint;

            dist = Vector3.Dot(v, nVector); // point to plane distance along nVector

            // Find new position
            newPos -= 2 * dist * nVector;

            // Find new velocity direction
            newDirection = newPos - intersectPoint;

            // Find new velocity
            velcty = Vector3.ClampMagnitude(newDirection, velcty.magnitude);
        }

        return (newPos, velcty);
    }
}
