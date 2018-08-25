using UnityEngine;
using System.Collections;

public class HammerScript : MonoBehaviour {

    GameManager gm;
	HammerSpawn spawnScript;
	public float level;
	float speed = 2f;


	void Awake () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		spawnScript = GameObject.Find ("HammerSpawner").GetComponent<HammerSpawn> ();
	}

	void Start () {
	}
	
		
	void Update () {

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //if the enemy went outside the screen on the bottom, then destroy the enemy
        if (transform.position.y < min.y || transform.position.x < min.x || transform.position.y > max.y || transform.position.x > max.x)
        {
            Destroy(gameObject);
        }
	}
}

