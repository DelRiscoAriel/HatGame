using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explode : MonoBehaviour
{
    public GameObject explotion;
    //public Transform pos;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Hat")
        {
            Instantiate(explotion, this.transform.position, Quaternion.identity);
        }
    }
}
