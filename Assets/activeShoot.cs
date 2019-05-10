using UnityEngine;
using System.Collections;
using UnityEngine.iOS;

public class activeShoot : MonoBehaviour
{

    public Rigidbody preFab;
    public GM gm;
    public float counter_gravity = 1f;
    public float forward_ratio = 1.0f;
    public float upward_ratio = 1.0f;

    public float minForce;
    public float maxForce;
    public float chargeTime;

    public float angle;
    public float x;
    public float y;

    public float timer;
    private bool start;
    
    // Use this for initialization
    void Start()
    {
        timer = -1.0f;
        start = false;
    }
    // Update is called once per frame
    /*void Update()
    {
        if (Input.touchCount  == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                timer = 0.0f;
            }
            if (t.phase == TouchPhase.Ended)
            {
                LaunchProjectile(timer);
                timer = -1.0f;
            }
        }
        if (timer != -1.0f)
        {
            timer += Time.deltaTime;
            ShowForce(timer);
        }            
    }
    */

    public void activate()
    {
        start = true;
    }

    public void deactivate()
    {
        start = false;
    }

    void Update()
    {
        if (!start) return;
        if (Input.GetMouseButtonDown(0))
            timer = 0.0f;
        if (Input.GetMouseButtonUp(0))
        {
           // RaycastHit hit;
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;
            angle = Mathf.Atan2(x - Screen.width / 2, Screen.height - y)*Mathf.Rad2Deg;

            LaunchProjectile(timer);
            timer = -1.0f;
        }
        if (timer != -1.0f)
        {
            timer += Time.deltaTime;
            ShowForce(timer);
        }
    }


    float GetForce(float time)
    {
        int loops = Mathf.FloorToInt (time / (chargeTime * 2));
        float remainder = time - loops * chargeTime * 2;
        if (remainder > chargeTime)
            return maxForce - ((maxForce - minForce) * (remainder - chargeTime) / chargeTime);
        else
            return minForce + ((maxForce - minForce) * remainder / chargeTime);
    }

    void ShowForce(float time)
    {
        gm.showTime((GetForce (time) - minForce) / (maxForce - minForce));
    }

    void LaunchProjectile(float time)
    {
        float speed = GetForce(time);
        Rigidbody rb = (Rigidbody)Instantiate(preFab, transform.position, transform.rotation);
        Vector3 raw_velocity = new Vector3(0, upward_ratio * counter_gravity, forward_ratio) * speed;
        rb.velocity = Quaternion.Euler(0, angle, 0) * raw_velocity;
    }
}
