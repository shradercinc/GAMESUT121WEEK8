using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyGridSystem : MonoBehaviour {

    public List<GridCell> gridCellList = new List<GridCell>();
    public int rows = 5;
    public int columns = 5;
    public float cellSize = 1.0f;
    System.Random r;

    public MyBuddy buddyPrefab;
    public int spawnCount;

    public float buddySpeed = 1.0f;
    public float pauseTime = 0.5f;

    private void Start() {
        DrawGrid();
        DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        r = new System.Random((int)(DateTime.Now - epochStart).TotalSeconds);

        for(int i = 0; i < spawnCount; i++) {
            MyBuddy b = Instantiate(buddyPrefab, GetRandomLocation(), Quaternion.identity);
        }
    }

    void DrawGrid() {

        float startX = ((-columns / 2.0f) * cellSize) + (cellSize / 2.0f);
        float startY = ((-rows / 2.0f) * cellSize) + (cellSize / 2.0f);

        for (int i = 0; i < rows; i++) {
            Debug.Log($"Hello Row {i}");

            for (int j = 0; j < columns; j++) {

                gridCellList.Add(new GridCell(
                            startX + (j * cellSize),
                            startY + (i * cellSize)
                            )
                           );
            }

        }

    }


    public Vector2 GetRandomLocation() {
        return gridCellList[ r.Next(gridCellList.Count) ].location;
    }

    private void OnDrawGizmos() {
        for(int i = 0; i < gridCellList.Count; i++) {
            Gizmos.DrawWireCube(gridCellList[i].location, Vector2.one * cellSize);
        }
    }


}

[System.Serializable]
public class GridCell {

    public Vector2 location;

    public GridCell() { }
    public GridCell(Vector2 l) {
        location = new Vector2(l.x, l.y);
    }
    public GridCell(float x, float y) {
        location = new Vector2(x, y);
    }

}