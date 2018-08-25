using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;
    public float speed = 6.0f;
    MCMove mc;
    void Start()
    {
        mc = GameObject.Find("MC Hammer").GetComponent<MCMove>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Reset();
    }


	void Update()
	{
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		if (!gm.doingSetup)
		{
			var move = new Vector3(0,0,0);
			move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			var total = move * speed * Time.deltaTime;
			if (transform.position.x + total.x <= max.x && transform.position.y + total.y <= max.y && transform.position.x + total.x >= min.x && transform.position.y + total.y >= min.y) {
				transform.position += total;
			}

		}
	}


    public void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.position = new Vector2(0, -3);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }


    IEnumerator SecondsDelay()
    {
        yield return new WaitForSeconds(1);
        GetComponent<PlayerMovement>().enabled = true;
    }

    IEnumerator FrameLoadDelay()
    {
        gm.RemoveTime();
        yield return new WaitForSeconds(.000001f);
        gm.RemoveTime();
        gm.LoadLevel();
    }

    IEnumerator FrameOverDelay()
    {
        gm.RemoveTime();
        yield return new WaitForSeconds(.000001f);
        gm.RemoveTime();
        gm.GameOver();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (!gm.doingSetup)
        {
            GameObject collidedWith = coll.transform.gameObject;

            if (collidedWith.name == "MC Hammer")
            {
                mc.move = false;
                StartCoroutine(FrameLoadDelay());
                GetComponent<PlayerMovement>().enabled = false;
                StartCoroutine(SecondsDelay());
                
            }
            else if (collidedWith.tag == "Obstacle")
            {
                Debug.Log("You collided with " + collidedWith.name);
                StartCoroutine(FrameOverDelay());
                GetComponent<PlayerMovement>().enabled = false;
                StartCoroutine(SecondsDelay());
            }
            
        }
    }
}