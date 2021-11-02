using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBuddy : MonoBehaviour {

    public float speed = 1.0f;
    public float pauseTime = 0.5f;

    Vector2 nextLocation = new Vector2();
    MyGridSystem gridSystem;
    SpriteRenderer spriteRenderer;

    public bool isActive;

    //Gettting the sprite renderer component
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// Start is called before the first frame update
    /// Setting the grid system & setting isActive to true
    void Start() {
        gridSystem = GameObject.FindGameObjectWithTag("GridSystem").GetComponent<MyGridSystem>();
        isActive = true;
        StartCoroutine(MoveToLocation());
    }

    /// <summary>
    /// While isActive is true, move this gameobject to a new location, wait 1 second
    /// then move it to a new location that it gets from the grid system.
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveToLocation() {

        while (isActive) {
            float t = 0.0f;
            nextLocation = gridSystem.GetRandomLocation();
            Vector2 startLocation = transform.position;
            spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            while (t < 1.0f) {
                t += Time.deltaTime * gridSystem.buddySpeed;
                transform.position = Vector2.Lerp(startLocation, nextLocation, t);
                yield return null;
            }
            yield return new WaitForSeconds(gridSystem.pauseTime);
        }

    }

}
