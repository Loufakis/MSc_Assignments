using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task4Behavior : MonoBehaviour
{
    public Task4Manager myManager;
    private float border = 5.0f;
    private float maxSpeed = 2.0f;
    private float maxForce = 0.03f;
    private float mass = 2.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 newPosition;
    private GameObject[] allBoids;

    private float deltatime = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(Random.Range(-100.0f, 100.0f),
                               Random.Range(-100.0f, 100.0f),
                               Random.Range(-100.0f, 100.0f)) * deltatime;
    }

    // Update is called once per frame
    void Update()
    {
        allBoids = myManager.allBoids;
        acceleration = Vector3.zero;

        Vector3 sep = Seperate(allBoids); // Separation
        Vector3 ali = Align(allBoids);    // Alignment
        Vector3 coh = Cohesion(allBoids); // Cohesion

        // Arbitrarily weight these forces
        sep *= myManager.seperationWeight;
        ali *= myManager.alignWeight;
        coh *= myManager.cohesionWeight;

        // Add the force vectors to acceleration
        acceleration += (sep + ali + coh) / mass;

        // Update
        velocity += acceleration;
        // Limit speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Wraparound
        newPosition = this.transform.position + (velocity * deltatime);
        newPosition = borders(newPosition);

        //Update position
        this.transform.position = newPosition;
    }

    public Vector3 borders(Vector3 position)
    {
        if (position.x < -2 * border) position.x += 3 * border;
        if (position.y < -1 * border) position.y += 3 * border;
        if (position.z < -1 * border) position.z += 3 * border;

        if (position.x > +2 * border) position.x -= 3 * border;
        if (position.y > +2 * border) position.y -= 3 * border;
        if (position.z > +3 * border) position.z -= 3 * border;

        return position;
    }

    public Vector3 Seperate(GameObject[] boids)
    {
        float desiredseparation = myManager.seperationDist;
        Vector3 steer = Vector3.zero;
        float nDistance;
        int count = 0;

        foreach (GameObject boid in boids)
        {
            if (boid != this.gameObject)
            {
                nDistance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (nDistance < desiredseparation)
                {
                    Vector3 diff = this.transform.position - boid.transform.position;
                    diff = Vector3.Normalize(diff);
                    diff /= nDistance;        // Weight by distance
                    steer += diff;
                    count++;
                }
            }
        }
        // Average -- divide by how many
        if (count > 0)
        {
            steer /= count;
        }

        // As long as the vector is greater than 0
        if (steer.magnitude > 0)
        {
            // First two lines of code below could be condensed with new PVector setMag() method
            // Not using this method until Processing.js catches up
            // steer.setMag(maxspeed);

            // Implement Reynolds: Steering = Desired - Velocity
            steer = Vector3.Normalize(steer);
            steer *= maxSpeed;
            steer -= velocity;
            steer = Vector3.ClampMagnitude(steer, maxForce);
        }

        return steer;
    }

    // Alignment
    public Vector3 Align(GameObject[] boids)
    {
        float neighbordist = myManager.cohesionDist;
        Vector3 sum = Vector3.zero;
        float nDistance;
        int count = 0;

        foreach (GameObject boid in boids)
        {
            if (boid != this.gameObject)
            {
                nDistance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (nDistance < neighbordist)
                {
                    sum += boid.GetComponent<Task4Behavior>().velocity; // SOSOSOSOSOSOSOSOSOSOSOS
                    count++;
                }
            }
        }

        if (count > 0)
        {
            sum /= count;
            // First two lines of code below could be condensed with new PVector setMag() method
            // Not using this method until Processing.js catches up
            // sum.setMag(maxspeed);

            // Implement Reynolds: Steering = Desired - Velocity
            sum = Vector3.Normalize(sum);
            sum *= maxSpeed;
            sum -= velocity;
            sum = Vector3.ClampMagnitude(sum, maxForce);

            return sum;
        }
        else
        {
            return Vector3.zero;
        }
    }

    // Cohesion
    public Vector3 Cohesion(GameObject[] boids)
    {
        float neighbordist = myManager.cohesionDist;
        Vector3 sum = Vector3.zero;
        float nDistance;
        int count = 0;

        foreach (GameObject boid in boids)
        {
            if (boid != this.gameObject)
            {
                nDistance = Vector3.Distance(boid.transform.position, this.transform.position);
                if (nDistance < neighbordist)
                {
                    sum += boid.transform.position;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= count;
            return Seek(sum);  // Steer towards the position
        }
        else
        {
            return Vector3.zero;
        }
    }

    // A method that calculates and applies a steering force towards a target
    // STEER = DESIRED MINUS VELOCITY
    public Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - this.transform.position;  // A vector pointing from the position to the target

        desired = Vector3.Normalize(desired);
        desired *= maxSpeed;  // Scale to maximum speed

        // Above two lines of code below could be condensed with new PVector setMag() method
        // Not using this method until Processing.js catches up
        // desired.setMag(maxspeed);

        // Steering = Desired minus Velocity
        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
}
