using UnityEngine;
using System.Collections;

public class LightningMove : MonoBehaviour
{
    public float speed;//for the enemy speed
    public GameManager gm;

    private Vector2 position;

    // Use this for initialization
    void Start()
    {
        speed = 2f; // set speed
    }

    // Update is called once per frame
    void Update()
    {
        //Get the enemy current position
        position = transform.position;

        //Compute the enemy new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //if the enemy went outside the screen on the bottom, then destroy the enemy
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.name == "Player" && gameObject.name == "yellow_lightning(Clone)")
        {
            print(gameObject.name);
            Destroy(gameObject);
        }
        if(coll.collider.name == "Player" && gameObject.name == "red_lightning(Clone)")
        {
            print(gameObject.name);
            Destroy(gameObject);
        }
    }
}
