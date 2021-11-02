using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySimpleRandomSpawn : MonoBehaviour {

    public GameObject blockPrefab;
    public int seed = 0;

    private void Start() {
        Random.InitState(seed);
    }


    void SpawnBlockObject() {

        float x = Random.Range(-8.0f, 8.0f);
        float y = Random.Range(-5.0f, 5.0f);
        GameObject b = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity);

    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnBlockObject();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
