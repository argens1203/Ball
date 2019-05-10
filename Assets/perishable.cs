using UnityEngine;
using System.Collections;

public class perishable : MonoBehaviour {

    private float lifeInSeconds = 5.0f;
    
	// Update is called once per frame
	void Update () {
        lifeInSeconds -= Time.deltaTime;
        if (lifeInSeconds < 0 || transform.position.y < -200)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
