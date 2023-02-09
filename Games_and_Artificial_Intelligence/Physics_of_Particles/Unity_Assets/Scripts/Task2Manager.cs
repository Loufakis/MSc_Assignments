using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task2Manager : MonoBehaviour
{
    public GameObject ParticlePrefab;
    public GameObject Magnet1Prefab;
    public GameObject Magnet2Prefab;

    private GameObject magnet1;
    private GameObject magnet2;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        magnet1 = Instantiate(Magnet1Prefab,new Vector3(-6.0f, -5.0f, 0.0f), Quaternion.identity);
        magnet1.transform.tag = "magnet1"; //Important: the tag must already exist!

        magnet2 = Instantiate(Magnet2Prefab, new Vector3(6.0f, -5.0f, 0.0f), Quaternion.identity);
        magnet2.transform.tag = "magnet2"; //Important: the tag must already exist!

        for (int i = 0; i < 4; i++)
        {
            GameObject instatiatedsSphere = Instantiate(ParticlePrefab);
            instatiatedsSphere.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.frameCount % 100 == 0) && (count < 20))
        { 
            // Every 100 frames a new particle is created
            GameObject instatiatedsSphere = Instantiate(ParticlePrefab);
            instatiatedsSphere.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            count++;
            
        }
    }
}
