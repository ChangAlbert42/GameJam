using UnityEngine;
using System.Collections;

public class RocketSpawner : MonoBehaviour {

    public GameManager gm;
    public GameObject RocketGO;
    public float maxSpawnRateInSeconds = 5f;
    public float spawnRate = 1f;
    public float maxSizeIncrease = 0;
    public float maxSizeIncreaseMax = 2;


	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.enabled = false;
	}


    void Update()
    {
        if (this.enabled && !gm.doingSetup)
        {
            spawnRate -= Time.deltaTime;
            if (spawnRate <= 0)
            {
                SpawnEnemy();
                spawnRate = 5f;
            }
        }
    }


    public void BeginSpawning()
    {
        Invoke("SpawnEnemy", 0);
    }


    void SpawnEnemy()
    {
        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //instantiate an enemy
        GameObject anEnemy = (GameObject)Instantiate(RocketGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y + 3.5f);

        float scaleNum = Random.Range(0, maxSizeIncrease);
        anEnemy.transform.localScale += new Vector3(scaleNum, scaleNum, 0);
    }

    void ScheduleNextEnemySpawn()
    {
        if (!gm.doingSetup)
        {
            Invoke("SpawnEnemy", Random.Range(1f, maxSpawnRateInSeconds));
        }
    }


    //function to increase the difficulty of the game
    //called from GameManager each level
    public void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f || MainMenuManager.impossible)
            maxSpawnRateInSeconds--;

        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");

        //also increases SIZE :)
        if (maxSizeIncrease <= maxSizeIncreaseMax || MainMenuManager.impossible)
        {
            maxSizeIncrease += 0.1f;
        }
    }
}

