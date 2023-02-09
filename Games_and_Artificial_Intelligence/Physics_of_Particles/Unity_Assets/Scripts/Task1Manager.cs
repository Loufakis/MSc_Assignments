using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1Manager : MonoBehaviour
{
    public GameObject SpherePrefab;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        { //stin arxi ftiaxnoume 4 sfaires gia na iparxoun ston xoro
            GameObject instatiatedsSphere = Instantiate(SpherePrefab);
            instatiatedsSphere.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.frameCount % 100 == 0) && (count < 100))
        { //kathe 300 frames prosthetoume sfaira
            GameObject instatiatedsSphere = Instantiate(SpherePrefab);
            instatiatedsSphere.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            count++;
        }
    }
}