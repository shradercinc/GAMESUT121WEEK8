using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 10.0f;

    [Header("loaded items")]
    public GameObject fish;
    public GameObject bandit;
    public GameObject wall;
    public GameObject bottomWall;
    public GameObject floor;
    public GameObject ammo;
    public GameObject guns;
    public GameObject rads;
    float prev = 0;
    float next = 0;
    int ammoRm = 0;
    int gunRm = 0;
    int radRm = 0;
    bool ammoRmNow = false;
    bool gunRmNow = false;
    bool radRmNow = false;
    public List<Vector2> enemSpawns = new List<Vector2>();
    public List<Vector2> chestSpawns = new List<Vector2>();

    public Transform startLocation;

    void SetSeed() {
        if (seedType == SeedType.RANDOM) {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) {
            random = new System.Random(seed);
        }
    }


    // yay here are the possible spawn rooms
    void PrevNulNextNul_Nul(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        chestSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        chestSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x, gridslot0.y));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }



        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x, gridslot0.y, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }


    //                              STARTING ROOMS
    //starting room, moving left
    void Prev0Next1_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity); 
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 6));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 1, -9), Quaternion.identity);
            
            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }
        
       
        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 3, gridslot0.y - 4, -9), Quaternion.identity);
        
        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev0Next1_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 4));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x + 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 8, -9), Quaternion.identity);


        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 2, gridslot0.y - 6, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting room, moving up
    void Prev0Next2_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1 , gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y - 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y - 5 + i, -9), Quaternion.identity); 

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }



        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 4, gridslot0.y - 7, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev0Next2_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 2));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y -4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y - 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - 1, -9), Quaternion.identity);

        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 2, gridslot0.y - 7, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting room, moving right
    void Prev0Next3_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 6));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y - 1, -9), Quaternion.identity);

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x - 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }


        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x - 3, gridslot0.y - 4, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev0Next3_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 4));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x - 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 8, -9), Quaternion.identity);


        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x - 2, gridslot0.y - 6, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting room, moving down
    void Prev0Next4_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y + 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y + 5 - i, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }



        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 4, gridslot0.y + 7, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev0Next4_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y + 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + 1, -9), Quaternion.identity);

        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 2, gridslot0.y + 7, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }


    //                              PREV 3 (FROM THE LEFT)
    //starting left, moving left
    void Prev3Next3_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 2, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 7));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y - 1, -9), Quaternion.identity);

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x - 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev3Next3_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 4));
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 2, gridslot0.y - 7));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x - 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 8, -9), Quaternion.identity);



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting left, moving up
    void Prev3Next2_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 7));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x - 3  - i, gridslot0.y - 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);

            
        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev3Next2_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 0));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 7, gridslot0.y - 6 - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y - 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting left, moving down
    void Prev3Next4_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 7));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y + 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev3Next4_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 0));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y + 6 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 7, gridslot0.y + 6 + i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y + 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y + 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }


    //                              PREV 1 (FROM THE RIGHT)
    //starting right, moving right
    void Prev1Next1_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 7));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 1, -9), Quaternion.identity);

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev1Next1_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 7));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x + 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 8, -9), Quaternion.identity);



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting right, moving up
    void Prev1Next2_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 7));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev1Next2_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 0));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y - 6 - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting right, moving down
    void Prev1Next4_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 7));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev1Next4_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 0));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + 6 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + 6 + i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //                              PREV 2 (FROM THE BOTTOM)

    //starting up, moving right
    void Prev2Next3_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 7));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev2Next3_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 0));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + 6 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + 6 + i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y + 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting up, moving left
    void Prev2Next1_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 7));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y + 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev2Next1_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y + 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y + 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y + 0));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y + 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y + 6 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 7, gridslot0.y + 6 + i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + 7 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y + 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y + 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y + 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting up, moving up
    void Prev2Next2_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 0));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 9));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y + 9, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y + 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y + 5 - i, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev2Next2_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 0));

        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y + 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y + 9, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + 1, -9), Quaternion.identity);


        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }


    //                              PREV 4 (FROM THE TOP)

    //starting down, moving left
    void Prev4Next3_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 7));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev4Next3_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 0, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 0));
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x + 9 - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2 - i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y - 6 - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3 + i, gridslot0.y - 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting down, moving right
    void Prev4Next1_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 7));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 8));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);

            //center cube
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 3, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 5, -9), Quaternion.identity);


        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);


        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 8, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev4Next1_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 7);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 3, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 0, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 6, gridslot0.y - 0));
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 3));
        enemSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);

            //exit wall left & right
            Instantiate(wall, new Vector3(gridslot0.x - 9 + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2 + i, gridslot0.y, -9), Quaternion.identity);


            Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 7, gridslot0.y - 6 - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - 7 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y - 7, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3 - i, gridslot0.y - 5, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //starting down, moving down
    void Prev4Next4_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {

        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y + 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y + 5 - i, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y + 9, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }


        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev4Next4_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {

        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y + 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y + 9, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + 1, -9), Quaternion.identity);

        //SpawnLocation for fish (only in prev0 rooms)
        Instantiate(fish, new Vector3(gridslot0.x + 2, gridslot0.y + 7, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //                              ENDING ROOMS
    //ending room, coming right
    void Prev3Next0_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 6));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 1, -9), Quaternion.identity);

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev3Next0_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 4));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x + 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - 8, -9), Quaternion.identity);



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //ending room, coming up
    void Prev4Next0_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y - 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y - 5 + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y - 5 + i, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev4Next0_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y - 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y - 2));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(bottomWall, new Vector3(gridslot0.x + i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y - 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y - 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y - 1, -9), Quaternion.identity);


        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //ending room, coming left
    void Prev1Next0_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 8));
        chestSpawns.Add(new Vector2(gridslot0.x - 4, gridslot0.y - 1));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 2, gridslot0.y - 8));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 6));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {
            Instantiate(wall, new Vector3(gridslot0.x - 1 - i, gridslot0.y - 1, -9), Quaternion.identity);

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x - 1, gridslot0.y - 1 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 6 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 6, gridslot0.y - 6 - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
        }



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev1Next0_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x + 4.5f, currentPos.y + 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 2));
        chestSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 7));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x - 1, gridslot0.y - 1));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 6));
        enemSpawns.Add(new Vector2(gridslot0.x - 7, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 8, gridslot0.y - 5));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 2));
        enemSpawns.Add(new Vector2(gridslot0.x - 5, gridslot0.y - 4));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x - i, gridslot0.y, -9), Quaternion.identity);
            Instantiate(bottomWall, new Vector3(gridslot0.x - i, gridslot0.y - 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y - i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall top
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - i, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {
            //exit wall bottom
            Instantiate(wall, new Vector3(gridslot0.x - 9, gridslot0.y - 7 - i, -9), Quaternion.identity);
            //bottom cube
            Instantiate(wall, new Vector3(gridslot0.x - 4, gridslot0.y - 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 5, gridslot0.y - 5 - i, -9), Quaternion.identity);
            //top cube
            Instantiate(wall, new Vector3(gridslot0.x - 2, gridslot0.y - 2 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x - 3, gridslot0.y - 2 - i, -9), Quaternion.identity);

        }

        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 1, -9), Quaternion.identity);
        Instantiate(wall, new Vector3(gridslot0.x - 8, gridslot0.y - 8, -9), Quaternion.identity);



        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    //ending room, coming down
    void Prev2Next0_1(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 6));
        chestSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));
        chestSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 4));
        enemSpawns.Add(new Vector2(gridslot0.x + 4, gridslot0.y + 5));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 2, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 8, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 7, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            //small corridor walls
            Instantiate(wall, new Vector3(gridslot0.x + 3, gridslot0.y + 5 - i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6, gridslot0.y + 5 - i, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }




        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }
    void Prev2Next0_2(Vector2 currentPos)
    {
        Instantiate(floor, new Vector3(currentPos.x, currentPos.y, -1), Quaternion.identity);
        int enemNo = (int)Random.Range(1, 5);
        Vector2 gridslot0 = new Vector2(currentPos.x - 4.5f, currentPos.y - 4.5f);

        //setting chest spawns
        chestSpawns.Clear();
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 5));
        chestSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 3));
        chestSpawns.Add(new Vector2(gridslot0.x + 1, gridslot0.y + 3));

        //setting enemy spawns
        enemSpawns.Clear();
        enemSpawns.Add(new Vector2(gridslot0.x + 8, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 6, gridslot0.y + 2));
        enemSpawns.Add(new Vector2(gridslot0.x + 7, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 5, gridslot0.y + 6));
        enemSpawns.Add(new Vector2(gridslot0.x + 2, gridslot0.y + 1));
        enemSpawns.Add(new Vector2(gridslot0.x + 3, gridslot0.y + 2));


        //Spawning the walls
        for (int i = 0; i < 10; i++)
        {
            //lengthwise walls
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y + 9, -9), Quaternion.identity);
        }
        for (int i = 0; i < 9; i++)
        {
            //heightwise walls
            Instantiate(wall, new Vector3(gridslot0.x, gridslot0.y + i, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 9, gridslot0.y + i, -9), Quaternion.identity);
        }
        for (int i = 0; i < 3; i++)
        {

            //exit wall left
            Instantiate(wall, new Vector3(gridslot0.x + i, gridslot0.y, -9), Quaternion.identity);

            Instantiate(wall, new Vector3(gridslot0.x + 1 + i, gridslot0.y + 4, -9), Quaternion.identity);
            Instantiate(wall, new Vector3(gridslot0.x + 6 + i, gridslot0.y + 4, -9), Quaternion.identity);

            //exit wall right
            Instantiate(wall, new Vector3(gridslot0.x + 7 + i, gridslot0.y, -9), Quaternion.identity);

        }
        for (int i = 0; i < 2; i++)
        {


        }

        Instantiate(wall, new Vector3(gridslot0.x + 1, gridslot0.y + 1, -9), Quaternion.identity);

        //spawning bandits
        for (int i = 0; i < enemNo; i++)
        {
            int spawnSelect = Random.Range(0, enemSpawns.Count);
            Instantiate(bandit, new Vector3(enemSpawns[spawnSelect].x, enemSpawns[spawnSelect].y, -9), Quaternion.identity);
            enemSpawns.RemoveAt(spawnSelect);
        }

        //Spawning chests
        if (ammoRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(ammo, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            ammoRmNow = false;
        }
        if (gunRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(guns, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            gunRmNow = false;
        }
        if (radRmNow == true)
        {
            int spawnSelect = Random.Range(0, chestSpawns.Count);
            Instantiate(rads, new Vector3(chestSpawns[spawnSelect].x, chestSpawns[spawnSelect].y, -9), Quaternion.identity);
            chestSpawns.RemoveAt(spawnSelect);
            radRmNow = false;
        }


    }

    void CreatePath() {

        GameObject[] gos = GameObject.FindGameObjectsWithTag("erase");
        foreach (GameObject go in gos)
            Destroy(go);
        gridCellList.Clear();

        ammoRm = Random.Range(0, 4);

        gunRm = Random.Range(0, 4);

        radRm = Random.Range(0, 4);

        Debug.Log($"ammo = {ammoRm}");
        Debug.Log($"gun = {gunRm}");
        Debug.Log($"rad = {radRm}");
        Vector2 currentPosition = startLocation.transform.position;
        for (int i = 0; i < pathLength; i++) 
        {
            Debug.Log($"i = {i}");

            if (i == ammoRm)
            {
                ammoRmNow = true;
            } else
            {
                ammoRmNow = false;
            } 

            if (i == gunRm)
            {
                gunRmNow = true;
            }
            else
            {
                gunRmNow = false;
            }

            if (i == radRm)
            {
                radRmNow = true;
            }
            else
            {
                radRmNow = false;
            }

            //check prev (the direction the previous path was going),
            //place a marker grid space in that direction
            //select a direction different than the opposite of prev (so as not to go backwards),
            //mark next as such
            //choose a tile that has an entrance based on prev, and an exit based next
            //set prev to next
            //repeat
            Debug.Log($"p; {prev}");
            Debug.Log($"n; {next}");
            if (prev != 0)
            {
                if (i == pathLength - 1)
                {
                    if (prev == 1)
                    {
                        currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
                        next = 0;
                        int ranlev = Random.Range(1, 3);
                        if (ranlev == 1)
                        {
                            Prev1Next0_1(currentPosition);
                        }
                        if (ranlev == 2)
                        {
                            Prev1Next0_2(currentPosition);
                        }
                    }
                    else if (prev == 2)
                    {
                        currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
                        next = 0;
                        int ranlev = Random.Range(1, 3);
                        if (ranlev == 1)
                        {
                            Prev2Next0_1(currentPosition);
                        }
                        if (ranlev == 2)
                        {
                            Prev2Next0_2(currentPosition);
                        }
                    }
                    else if (prev == 3)
                    {
                        currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
                        next = 0;
                        int ranlev = Random.Range(1, 3);
                        if (ranlev == 1)
                        {
                            Prev3Next0_1(currentPosition);
                        }
                        if (ranlev == 2)
                        {
                            Prev3Next0_2(currentPosition);
                        }
                    }
                    else if (prev == 4)
                    {
                        currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
                        next = 0;
                        int ranlev = Random.Range(1, 3);
                        if (ranlev == 1)
                        {
                            Prev4Next0_1(currentPosition);
                        }
                        if (ranlev == 2)
                        {
                            Prev4Next0_2(currentPosition);
                        }
                    }
                    gridCellList.Add(new MyGridCell(currentPosition));
                }
                if (i != pathLength - 1)
                {
                    if (prev == 1)
                    {
                        currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
                        next = (int)Random.Range(1, 5);
                        while (next == 3)
                        {
                            next = (int)Random.Range(1, 5);
                        }
                        if (next == 1)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev1Next1_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev1Next1_2(currentPosition);
                            }
                        }
                        if (next == 2)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev1Next2_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev1Next2_2(currentPosition);
                            }
                        }
                        if (next == 4)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev1Next4_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev1Next4_2(currentPosition);
                            }
                        }
                    }
                    else if (prev == 2)
                    {
                        currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
                        next = (int)Random.Range(1, 5);
                        while (next == 4)
                        {
                            next = (int)Random.Range(1, 5);
                        }
                        if (next == 1)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev2Next1_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev2Next1_2(currentPosition);
                            }
                        }
                        if (next == 2)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev2Next2_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev2Next2_2(currentPosition);
                            }
                        }
                        if (next == 3)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev2Next3_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev2Next3_2(currentPosition);
                            }
                        }
                    }
                    else if (prev == 3)
                    {
                        currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
                        next = (int)Random.Range(1, 5);
                        while (next == 1)
                        {
                            next = (int)Random.Range(1, 5);
                        }
                        if (next == 2)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev3Next2_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev3Next2_2(currentPosition);
                            }
                        }
                        if (next == 3)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev3Next3_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev3Next3_2(currentPosition);
                            }
                        }
                        if (next == 4)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev3Next4_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev3Next4_2(currentPosition);
                            }
                        }
                    }
                    else if (prev == 4)
                    {
                        currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
                        next = Random.Range(1, 5);
                        while (next == 2)
                        {
                            next = Random.Range(1, 5);
                        }
                        if (next == 1)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev4Next1_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev4Next1_2(currentPosition);
                            }
                        }
                        if (next == 3)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev4Next3_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev4Next3_2(currentPosition);
                            }
                        }
                        if (next == 4)
                        {
                            int ranlev = Random.Range(1, 3);
                            if (ranlev == 1)
                            {
                                Prev4Next4_1(currentPosition);
                            }
                            if (ranlev == 2)
                            {
                                Prev4Next4_2(currentPosition);
                            }
                        }
                    }
                    gridCellList.Add(new MyGridCell(currentPosition));
                }
            }
            if (prev == 0)
            {
                next = Random.Range(1, 5);
                currentPosition = new Vector2(currentPosition.x, currentPosition.y);
                gridCellList.Add(new MyGridCell(currentPosition));
                if (next == 1)
                {
                    int ranlev = Random.Range(1, 3);
                    if (ranlev == 1)
                    {
                        Prev0Next1_1(currentPosition);
                    }
                    if (ranlev == 2)
                    {
                        Prev0Next1_2(currentPosition);
                    }
                }
                if (next == 2)
                {
                    int ranlev = Random.Range(1, 3);
                    if (ranlev == 1)
                    {
                        Prev0Next2_1(currentPosition);
                    }
                    if (ranlev == 2)
                    {
                        Prev0Next2_2(currentPosition);
                    }
                }
                if (next == 3)
                {
                    int ranlev = Random.Range(1, 3);
                    if (ranlev == 1)
                    {
                        Prev0Next3_1(currentPosition);
                    }
                    if (ranlev == 2)
                    {
                        Prev0Next3_2(currentPosition);
                    }
                }
                if (next == 4)
                {
                    int ranlev = Random.Range(1, 3);
                    if (ranlev == 1)
                    {
                        Prev0Next4_1(currentPosition);
                    }
                    if (ranlev == 2)
                    {
                        Prev0Next4_2(currentPosition);
                    }
                }

            }
            prev = next;
            
        }
    }

    IEnumerator CreatePathRoutine() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 49)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
            yield return null;
        }
    }



    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetSeed();

            /*
            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
            */
            prev = 0;
            next = 0;
            CreatePath();
        }
    }
}
