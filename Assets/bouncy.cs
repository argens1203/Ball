using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class bouncy : MonoBehaviour
{

    public float bounciness = 0.9f;
    Collider coll;

    private void OnEnable()
    {
        coll = GetComponent<Collider>();
        if (bounciness < 0) bounciness = 0;
        if (bounciness > 1) bounciness = 1;
        coll.material.bounciness = bounciness;
    }
}
