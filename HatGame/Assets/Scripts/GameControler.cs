using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public Camera cam;
    public GameObject[] ball;

    public float timeLeft;
    public Text timerText;
    public Text scoreText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject splashScreen;
    public GameObject startButton;
    public HatController hatController;

    private bool playing;
    private float maxWidth;

    public float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Ceck for Camera if null assign main camera
        if (cam == null)
            cam = Camera.main;

        playing = false;

        //Create a Vector3 to detrmine the upper corner of the screen
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);

        //Create a Vector3 to define the target Width of the moveemnt capabilities
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);

        //Create a hat width based on the hat sprite
        float ballWidth = ball[0].GetComponent<Renderer>().bounds.extents.x;

        //Assign the max width variable ro the target width's x value while subtracting the hat width
        maxWidth = targetWidth.x - ballWidth;

        UpdateText();
    }

    void FixedUpdate()
    {
        if (hatController.health.value < 10)
            timeLeft = 0;

        if(playing)
        {
            if (timeLeft <= 0)
                timeLeft = 0;

            timeLeft -= Time.deltaTime;
            UpdateText();
        }
    }

    void UpdateText()
    {
        timerText.text = "Time Left: \n" + Mathf.RoundToInt(timeLeft);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(speed);

        while(timeLeft > 0)
        {
            GameObject ballSpawn = ball[Random.Range(0, ball.Length)];

            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f);

            //Set the spawn rotation to default (0)
            Quaternion spawnRotation = Quaternion.identity;

            //Instantiate the ball and spawn it
            Instantiate(ballSpawn, spawnPosition, spawnRotation);

            //Wait a random amount of sedonds between .5 and 1,5 before next spawn
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }

        //yield return new WaitForSeconds(1.0f);
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        hatController.toggleControl(false);
    }

    public void StartGame()
    {
        splashScreen.SetActive(false);
        startButton.SetActive(false);
        hatController.toggleControl(true);
        playing = true;
        //Start a coroutine 
        StartCoroutine(Spawn());
    }
}
