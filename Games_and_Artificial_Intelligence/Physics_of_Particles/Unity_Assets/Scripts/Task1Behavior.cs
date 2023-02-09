using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Behavior : MonoBehaviour
{
    private Vector3 newPosition;
    private Vector3 velocity;
    private Vector3 acceleration;

    private float m = 3.0f;

    private float g = 0.98f;
    private Vector3 GravityForce = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 fGravity = new Vector3(0.0f, 0.0f, 0.0f);

    private float k = 2.5f;
    private Vector3 fDrag = new Vector3(0.0f, 0.0f, 0.0f);

    private float boundary = 5.0f;

    private float deltatime = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        GravityForce.y = -g;
        velocity = new Vector3(Random.Range(-50.0f, 50.0f),
                               Random.Range(-0.0f, 150.0f),
                               Random.Range(-50.0f, 50.0f)) * deltatime; //Time.deltaTime; 
    }

    // Update is called once per frame
    void Update()
    {
        FindNewSpeed();
        FindNewPosition();
        CheckForCollision();
        // Updating particle position
        this.transform.position = newPosition;
    }

    private void FindNewSpeed()
    {
        // Forces calculation
        fGravity = GravityForce * deltatime; //Time.deltaTime;
        fDrag = (-k * velocity) / m * deltatime; //Time.deltaTime;

        // Acceleration update according to applied forces
        acceleration = fGravity + fDrag;

        // Velocity update
        velocity += acceleration;
    }

    private void FindNewPosition()
    {
        newPosition = this.transform.position;
        newPosition += velocity;
    }

    private void CheckForCollision()
    {
        // Collision for the x axis
        if (newPosition.x < -boundary)
        {
            float dev = -boundary - newPosition.x;
            newPosition += new Vector3(2 * dev, 0.0f, 0.0f);
            velocity.x *= -1;
        }
        if (newPosition.x > boundary)
        {
            float dev = newPosition.x - boundary;
            newPosition -= new Vector3(2 * dev, 0.0f, 0.0f);
            velocity.x *= -1;
        }

        // Collision for the y axis
        if (newPosition.y < -boundary)
        {
            float dev = -boundary - newPosition.y;
            newPosition += new Vector3(0.0f, 2 * dev, 0.0f);
            velocity.y *= -1;
        }
        if (newPosition.y > boundary)
        {
            float dev = newPosition.y - boundary;
            newPosition -= new Vector3(0.0f, 2 * dev, 0.0f);
            velocity.y *= -1;
        }

        // Collision for the z axis
        if (newPosition.z < -boundary)
        {
            float dev = -boundary - newPosition.z;
            newPosition += new Vector3(0.0f, 0.0f, 2 * dev);
            velocity.z *= -1;
        }
        if (newPosition.z > boundary)
        {
            float dev = newPosition.z - boundary;
            newPosition -= new Vector3(0.0f, 0.0f, 2 * dev);
            velocity.z *= -1;
        }
    }
}
