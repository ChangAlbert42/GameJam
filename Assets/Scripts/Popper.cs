using UnityEngine;
using System.Collections;

public class Popper : MonoBehaviour {

    public GameObject confetti;
    public bool left;
    public int numOfConf = 4;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < numOfConf; i++)
        {
            GameObject newConf = (GameObject) Instantiate(confetti, this.transform.position, Quaternion.identity);
            newConf.GetComponent<Confetti>().left = this.left;
        }
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void increaseConf()
    {
        if(numOfConf < 7)
        {
            numOfConf += 1;
        }
    }
    
}
