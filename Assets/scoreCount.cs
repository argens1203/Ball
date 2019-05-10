using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class scoreCount : MonoBehaviour {

    public GM gm;
    public int score;

    private void OnTriggerEnter(Collider other)
    {
        gm.addScore(score);
    }
}
