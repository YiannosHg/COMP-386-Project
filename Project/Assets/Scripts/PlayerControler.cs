using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed; // The speed the ball moves
    public Text countText; // The text (count) to be shown 
    public Text winText; // The text to be shown when game finishes

    private float xInput;
    private float zInput;

    private int count; // counter of PicKup objects collected

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // Initialize counter
        countText.text = "Count: " + count.ToString(); // Show count
        winText.text = ""; // Set winText to empty string
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        // Movement
        Move();
    }

    // Gets the input for movement direction
    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
    }

    // Moves the player based on the movement direction
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0.0f, zInput) * moveSpeed);
    }

    // Collision detection to deactivate the PickUp object we touched
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            ++count;
            countText.text = "Count: " + count.ToString();

            if (count == 12)
            {
                winText.text = "You Win!";
            }
        }
    }
}
