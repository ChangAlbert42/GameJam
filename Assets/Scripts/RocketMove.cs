﻿using UnityEngine;
using System.Collections;

public class RocketMove : MonoBehaviour {

    public float speed;//for the enemy speed

    // Use this for initialization
    void Start () {
        speed = 4f; // set speed
	}
	
	// Update is called once per frame
	void Update () {
        //Get the enemy current position
        Vector2 position = transform.position;

        //Compute the enemy new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //if the enemy went outside the screen on the bottom, then destroy the enemy
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
	}
}
