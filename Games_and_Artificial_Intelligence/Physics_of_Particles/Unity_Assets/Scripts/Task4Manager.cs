using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task4Manager : MonoBehaviour
{
    public GameObject boidPrototype;
    public int n_Boids = 40;
    public GameObject[] allBoids;
    private float border = 5.0f;

    [Range(0.0f, 3.0f)]
    public float seperationWeight;
    [Range(0.0f, 3.0f)]
    public float alignWeight;
    [Range(0.0f, 3.0f)]
    public float cohesionWeight;

    [Range(0.0f, 50.0f)]
    public float seperationDist;
    [Range(0.0f, 100.0f)]
    public float cohesionDist;

    // Start is called before the first frame update
    void Start()
    {
        allBoids = new GameObject[n_Boids];
        for (int i = 0; i < n_Boids; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-2 * border, 2 * border), //x
                                                                Random.Range(-2 * border, 2 * border), //y
                                                                Random.Range(-2 * border, 2 * border));//z

            allBoids[i] = (GameObject)Instantiate(boidPrototype, pos, Quaternion.identity);
            allBoids[i].GetComponent<Task4Behavior>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
