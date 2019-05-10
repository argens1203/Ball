using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class gravity : MonoBehaviour {

    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;
    Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;    
    }
    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
