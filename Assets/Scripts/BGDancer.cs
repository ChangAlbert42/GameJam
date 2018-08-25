using UnityEngine;
using System.Collections;

public class BGDancer : MonoBehaviour
{
    public Transform center;
    public float degreesPerSecond = -20.0f;
    public GameManager gm;
    MCMove mc;

    private Vector3 v;

    // Use this for initialization
    void Start()
    {
        mc = GameObject.Find("MC Hammer").GetComponent<MCMove>();
        v = transform.position - center.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(mc.move)
        {
            v = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, Vector3.forward) * v;
            transform.position = center.position + v;
        }
        
    }
}
