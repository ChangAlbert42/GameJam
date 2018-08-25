using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public GameManager gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        gm.GameOver();
    }
}
