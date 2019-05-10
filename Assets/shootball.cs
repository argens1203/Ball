using UnityEngine;
using System.Collections;

public class shootball : MonoBehaviour {

    public Rigidbody preFab;
    public float speed = 100f;
    public float counter_gravity = 1f;
    public float startTime = 3f;
    public float period = 50f;
    public float forward_ratio = 1.0f;
    public float upward_ratio = 1.0f;
	// Use this for initialization
	void Start () {
        InvokeRepeating("LaunchProjectile", startTime, period);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void LaunchProjectile()
    {
        Rigidbody rb = (Rigidbody)Instantiate(preFab, transform.position, transform.rotation);
        rb.velocity = new Vector3(0,upward_ratio * counter_gravity,forward_ratio) * speed;
    }
}
