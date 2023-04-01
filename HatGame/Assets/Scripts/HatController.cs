using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatController : MonoBehaviour
{
    public Camera cam;

    private float maxWidth;
    private bool canControl;

    public Slider health;

    // Start is called before the first frame update
    void Start()
    {
        //Ceck for Camera if null assign main camera
        if (cam == null)
            cam = Camera.main;

        canControl = false;

        //Create a Vector3 to detrmine the upper corner of the screen
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);

        //Create a Vector3 to define the target Width of the moveemnt capabilities
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);

        //Create a hat width based on the sprite
        float hatWidth = GetComponent<Renderer>().bounds.extents.x;

        //Assign the max width variable to the target width's x value while subtractin the hat width
        maxWidth = targetWidth.x - hatWidth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canControl)
        {
            //Create a vector3 position representing the raw position of the hat
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            //Create a vector3 representing the position the hat should be at based off the raw location
            Vector3 targetPosition = new Vector3(rawPosition.x, -3.0f, 0.0f);

            //Create a float value to represent the target width by clamping the hat to the sceen bounds
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);

            //Assign the target position to a new Vector3
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);

            //Move the hat using Rigibody
            GetComponent<Rigidbody2D>().MovePosition(targetPosition);
        }
    }

    public void toggleControl(bool value)
    {
        canControl = value;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            health.value -= 20;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Health")
        {
            health.value += 20;
            Destroy(collision.gameObject);
        }
    }
}
