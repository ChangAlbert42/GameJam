using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 
using UnityEngine.UI;                   //Allows us to use UI.
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = .2f;                      //Time to wait before starting level, in seconds.
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public bool doingSetup = true;
    public int level;

    private Text levelText;                                 //Text to display current level number.
    private GameObject levelImage;                          //Image to block out level as levels are being set up, background for levelText.
    private GameObject playAgainButton;
    private GameObject giveUpButton;
    private GameObject player;
    private GameObject mcHammer;

    private string[] levelProgression = {"", "BG Spawner", "MCMove", "HammerSpawner", "PopperSpawner", "RocketSpawner"};
    private CreateDancer DancerSpawner;
    private HammerSpawn HammerSpawner;
    private PopperSpawner PopperSpawner;
    private RocketSpawner RocketSpawner;
    private MCMove MCMove;

    private bool impossibleSpawned = false;

    //SCORE - score calculation parameter
    public int winScore = 500;
    public float scoreRate = 1.5f;

    GameObject scoreUITextGO; //SCORE - reference to the text UI game object
    GameObject scoreUITextGO2; //SCORE - reference to the text UI game object
    GameObject scoreUITextGO3; //SCORE - reference to the text UI game object
    GameObject imgNewGO;


    void Awake()
    {
        Debug.Log("Awake called");
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        player = GameObject.Find("Player");
        mcHammer = GameObject.Find("MC Hammer");
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        playAgainButton = GameObject.Find("PlayAgainButton");
        giveUpButton = GameObject.Find("GiveUpButton");

        DancerSpawner = GameObject.Find("BG Spawner").GetComponent<CreateDancer>();
        HammerSpawner = GameObject.Find("HammerSpawner").GetComponent<HammerSpawn>();
        PopperSpawner = GameObject.Find("Popper Spawner").GetComponent<PopperSpawner>();
        RocketSpawner = GameObject.Find("RocketSpawner").GetComponent<RocketSpawner>();
        MCMove = GameObject.Find("MC Hammer").GetComponent<MCMove>();

        //SCORE - get the score text UI
        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
        scoreUITextGO2 = GameObject.FindGameObjectWithTag("ScoreText2Tag");
        scoreUITextGO3 = GameObject.FindGameObjectWithTag("ScoreHiTextTag");
        imgNewGO = GameObject.Find("NEW");
       
        //SCORE - initialize
        scoreUITextGO.GetComponent<GameScore>().Score = 0;
        //SCORE - hide hiscore features
        imgNewGO.SetActive(false);
       

        levelImage.SetActive(false);
        playAgainButton.SetActive(false);
        giveUpButton.SetActive(false);

        level = 1;
        InitGame();
    }

    //This is called each time a scene is loaded.
    public void LoadLevel()
    {
        HammerSpawner.SpeedUp();
        MCMove.SpeedUp();
        if (level % 3 == 0)
        {
            DancerSpawner.increaseDancer();
        }

        if (level % 5 == 0)
        {
            PopperSpawner.increaseSpawn();
        }


        MCMove.Reset();
        player.GetComponent<PlayerMovement>().Reset();

        //SCORE - score update
        scoreUITextGO.GetComponent<GameScore>().Score += (int)(winScore * Mathf.Pow(scoreRate, level));

        level++;
        InitGame();
        
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //SCORE - transition
        scoreUITextGO2.GetComponent<GameScore>().Score = scoreUITextGO.GetComponent<GameScore>().Score;
        if (PlayerPrefs.HasKey("HiScorePlayerPrefs"))
        {
            scoreUITextGO3.GetComponent<GameScore>().Score = PlayerPrefs.GetInt("HiScorePlayerPrefs");
        }
        else
        {
            scoreUITextGO3.GetComponent<GameScore>().Score = 0;
        }
        imgNewGO.SetActive(false);

        doingSetup = true;
        levelText.text = "Level " + level;
        levelImage.SetActive(true);
        DestroyAllObstacles();
        Invoke("HideLevelImage", levelStartDelay);
        Invoke("InitObstacles", 0.1f);
    }


    void InitObstacles()
    {
        if (MainMenuManager.impossible && !impossibleSpawned)
        {
            SpawnImpossible();
        }

        DancerSpawner.Spawn();

        if (!MainMenuManager.impossible)
        {
            if (level >= levelProgression.Length)
            {
                return;
            }

            string currObstacle = levelProgression[level];

            if (currObstacle == "RocketSpawner")
            {
                //RocketSpawner.active = true;
                RocketSpawner.enabled = true;
            }
            else if (currObstacle == "MCMove")
            {
                //MCMove.active = true;
                MCMove.enabled = true;
            }
            else if (currObstacle == "HammerSpawner")
            {
                //HammerSpawner.active = true;
                HammerSpawner.enabled = true;
            }
            else if (currObstacle == "PopperSpawner")
            {
                //PopperSpawner.active = true;
                PopperSpawner.enabled = true;
            }

            RocketSpawner.IncreaseSpawnRate();
        }
    }


    void SpawnImpossible()
    {
        Debug.Log("IMPOSSIBLE MODE ENABLED");
        MCMove.enabled = true;
        MCMove.speed = MCMove.maxSpeed;
        DancerSpawner.spawnMax = DancerSpawner.spawnMaxMax;

        HammerSpawner.enabled = true;
        HammerSpawner.spawnRate = HammerSpawner.maxSpawnRate;

        PopperSpawner.enabled = true;

        RocketSpawner.enabled = true;
        RocketSpawner.spawnRate = RocketSpawner.maxSpawnRateInSeconds;
        RocketSpawner.maxSizeIncrease = RocketSpawner.maxSizeIncreaseMax;

        impossibleSpawned = true;
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
        //MCMove.Reset();
        MCMove.RandomDirection();
        MCMove.move = true;
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        doingSetup = false;
    }
    
    //Update is called every frame.
    void Update()
    {
        
        if (doingSetup)
            return;
        //SCORE - decrease score
        if (scoreUITextGO.GetComponent<GameScore>().Score > 0)
            scoreUITextGO.GetComponent<GameScore>().Score--;
    }


    public void GameOver()
    {
        Camera.main.GetComponent<AudioSource>().Pause();
        playAgainButton.GetComponent<AudioSource>().Play();
        Invoke("PlayMusic", 1.6f);

        DisableAll();
        DestroyAllObstacles();
        levelText.text = "YOU CAN'T TOUCH THIS!";
               

        //SCORE - FINAL
        scoreUITextGO2.GetComponent<GameScore>().Score = scoreUITextGO.GetComponent<GameScore>().Score;
        if (scoreUITextGO3.GetComponent<GameScore>().Score < scoreUITextGO2.GetComponent<GameScore>().Score)
        {
            scoreUITextGO3.GetComponent<GameScore>().Score = scoreUITextGO2.GetComponent<GameScore>().Score;
            PlayerPrefs.SetInt("HiScorePlayerPrefs", scoreUITextGO3.GetComponent<GameScore>().Score);
            PlayerPrefs.Save();
            imgNewGO.SetActive(true);
        }

        levelImage.SetActive(true);
        playAgainButton.SetActive(true);
        giveUpButton.SetActive(true);
    }


    public void PlayMusic()
    {
        Camera.main.GetComponent<AudioSource>().Play();
    }


    public void PlayAgain()
    {
        mcHammer.GetComponent<MCMove>().Reset();
        player.GetComponent<PlayerMovement>().Reset();
        if (!MainMenuManager.impossible)
        {
            DisableAll();
        } else
        {
            impossibleSpawned = false;
        }
        level = 1;
        scoreUITextGO.GetComponent<GameScore>().Score = 0;
        playAgainButton.SetActive(false);
        giveUpButton.SetActive(false);
        InitGame();
    }

    


    public void DisableAll()
    {
        doingSetup = true;

        MCMove.enabled = false;
        HammerSpawner.enabled = false;
        PopperSpawner.enabled = false;
        RocketSpawner.enabled = false;

        foreach(GameObject moving in GameObject.FindGameObjectsWithTag("Obstacle")) {
            BGDancer movementDancer = moving.GetComponent<BGDancer>();
            if (movementDancer != null)
            {
                movementDancer.enabled = false;
            }
        }
    }
    

    IEnumerator SecDelay()
    {
        MCMove.move = false;
        Time.timeScale = 0f;
        float realTimeToUnpause = Time.realtimeSinceStartup + .5f;

        while (Time.realtimeSinceStartup < realTimeToUnpause)
        {
            yield return null;
        }
        
        Time.timeScale = 1.0f;
    }


    public void RemoveTime()
    {
        StartCoroutine(SecDelay());   
    }

    public void DestroyAllObstacles()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void GiveUp()
    {
        SceneManager.LoadScene("Main Menu");
    }
}