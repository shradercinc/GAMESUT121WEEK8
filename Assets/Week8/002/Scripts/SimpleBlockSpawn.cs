using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleBlockSpawn : MonoBehaviour {

    public GameObject blockPrefab;

    // Start is called before the first frame update
    void Start() {
        Random.InitState(7);
    }

    // Update is called once per frame
    void Update() {


        if (Input.GetKeyDown(KeyCode.Space)) {

            float x = Random.Range(-8.0f, 8.0f);
            float y = Random.Range(-5.0f, 5.0f);
            GameObject b = Instantiate(blockPrefab, new Vector3(x, y), Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}
