using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleBlockSpawnTwo : MonoBehaviour
{
    System.Random r;
    public GameObject blockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        r = new System.Random(100);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            float x = (float)(r.NextDouble() * (8.0f - (-8.0f)) + (-8.0f));
            float y = (float)(r.NextDouble() * (5.0f - (-5.0f)) + (-5.0f));
            GameObject b = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity);
        }
    }
}
