using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int ballValue;

    public int score;

    public bool onLevel2 = false;

    void Start()
    {
        score = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bow")
        {
            score += ballValue;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        if (score < 0)
            score = 0;

        if (onLevel2 == false)
        {
            if (score >= 10)
            {
                SceneManager.LoadScene("Level2");
                //onLevel2 = true;
            }               
        }
        if (onLevel2 == true)
        {
            if (score >= 10)
            {
                SceneManager.LoadScene("Main");
                //onLevel2 = false;
            }
        }

        scoreText.text = "Score:\n" + score + "/10";
    }
}
