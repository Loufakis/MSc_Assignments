using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task3Manager : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public int n_particles;
    public GameObject[] allParticles;

    [Range(0.0f, 3.5f)]
    public float repulseWeight;

    // Start is called before the first frame update
    void Start()
    {
        allParticles = new GameObject[n_particles];
        for (int i = 0; i < n_particles; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-0.005f, 0.005f),
                                      Random.Range(-0.005f, 0.005f),
                                      Random.Range(-0.005f, 0.005f));

            allParticles[i] = (GameObject)Instantiate(ParticlePrefab, pos, Quaternion.identity);
            allParticles[i].GetComponent<Task3Behavior>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
