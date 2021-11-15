using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandom : MonoBehaviour
{
    public Vector2 position = new Vector2(0, 0);
    private Transform pos;



    private void Awake()
    {
        pos = GetComponent<Transform>();
        //pos.transform.position = new Vector3(Random.Range(WidthMax - 15, WidthMin + 15), Random.Range(HeightMax - 15, HeightMin + 15), 0);
        pos.transform.position = new Vector3((int)Random.Range(-12, 12), (int)Random.Range(-5, 5), 0);
        Debug.Log("Position set");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pos.transform.position = new Vector3((int)Random.Range(-12, 12), (int)Random.Range(-5, 5), 0);
            Debug.Log("Position set");
        }

    }
}
