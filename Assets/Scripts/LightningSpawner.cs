using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{

    public GameObject yellow_lightning; //this is our object prefab
    public GameObject red_lightning;
    public float maxSpawnRateInSeconds = 5f;
    public Transform center;
    public GameManager gm;


    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnEnemy", Random.Range(0F,3F),Random.Range(2F,4F));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //function to spawn an enemy
    void SpawnEnemy()
    {
        //ScheduleNextEnemySpawn();
        float num = Random.Range(0F, 2F);
        if (num < 1)
        {
            GameObject aBolt = (GameObject)Instantiate(yellow_lightning);
            aBolt.transform.position = center.transform.position;
        }
        else
        {
            GameObject aBolt = (GameObject)Instantiate(red_lightning);
            aBolt.transform.position = center.transform.position;
        }
        //instantiate an enemy
        
    }
}

