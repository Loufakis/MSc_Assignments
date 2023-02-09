using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2Behavior : MonoBehaviour
{
    private Vector3 position;
    private Vector3 newPosition;
    private Vector3 velocity;
    private Vector3 acceleration;

    private float m = 3.0f;

    private float g = 0.98f;
    private Vector3 GravityForce = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 fGravity = new Vector3(0.0f, 0.0f, 0.0f);

    private float k = 2.5f;
    private Vector3 fDrag = new Vector3(0.0f, 0.0f, 0.0f);

    private GameObject magnet1;
    private Vector3 magnet1Position;
    private Vector3 distance1;
    public float magnet1Power = 1.0f;
    private Vector3 fMagnet1;

    private GameObject magnet2;
    private Vector3 magnet2Position;
    private Vector3 distance2;
    public float magnet2Power = 1.0f;
    private Vector3 fMagnet2;

    private float boundary = 5.0f;

    private float deltatime = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        magnet1 = GameObject.FindWithTag("magnet1");
        magnet2 = GameObject.FindWithTag("magnet2");

        GravityForce.y = -g;
        velocity = new Vector3(Random.Range(-50.0f, 50.0f),
                               Random.Range(-00.0f, 150.0f),
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
        fGravity = GravityForce * m;

        fDrag = -(k * velocity);

        position = this.transform.position;

            // Magnet 1 Force
        magnet1Position = magnet1.transform.position;
        distance1 = magnet1Position - position;
        fMagnet1 = magnet1Power * distance1 / Mathf.Pow(distance1.magnitude, 3.0f);

            // Magnet 1 Force
        magnet2Position = magnet2.transform.position;
        distance2 = magnet2Position - position;
        fMagnet2 = magnet2Power * distance2 / Mathf.Pow(distance2.magnitude, 3.0f);

        // Acceleration update according to applied forces
        acceleration = (fGravity + fDrag + fMagnet1 + fMagnet2) / m;

        // Velocity update
        velocity += acceleration * deltatime; //Time.deltaTime;
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
