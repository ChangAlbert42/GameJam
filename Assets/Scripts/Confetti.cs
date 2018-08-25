using UnityEngine;
using System.Collections;

public class Confetti : MonoBehaviour {
    public bool left;

    // Use this for initialization
    void Start () {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;

        Vector2 direction;

        if (left)
        {
            direction = new Vector2(1.0f, Random.Range(3.0f, 6.0f));
        } else
        {
            direction = new Vector2(-1.0f, Random.Range(3.0f, 6.0f));
        }

        this.GetComponent<Rigidbody2D>().AddForce(direction * 100f);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    
}
