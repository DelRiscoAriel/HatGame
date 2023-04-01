using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContactLevel2 : MonoBehaviour
{
    public Score score;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bow")
        {
            score.score -= 1;
            Debug.Log(score.score);
        }

        Destroy(other.gameObject);
    }
}
