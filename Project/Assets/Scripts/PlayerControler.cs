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

    // Sounds
    public AudioClip collectPickUp;
    public AudioClip collectDoNotPickUp;
    private AudioSource sound;

    // Movemnet
    private float xInput;
    private float zInput;

    private int count; // counter of PicKup objects collected

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        count = 0; // Initialize counter
        countText.text = "Count: " + count.ToString(); // Show count
        winText.text = ""; // Set winText to empty string
        sound = GetComponent<AudioSource>();
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

    // Collision detection to deactivate the PickUp object we touched and make sound
    // and lose the game if we touched a DoNotPickUp object and make sound
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            sound.clip = collectPickUp; // Set sound value
            sound.Play(); // Play sound
            other.gameObject.SetActive(false);
            ++count;
            countText.text = "Count: " + count.ToString();

            if (count == 12)
            {
                winText.text = "You Win!";
            }
        }
        else if(other.gameObject.CompareTag("DoNotPickUp"))
        {
            sound.clip = collectDoNotPickUp; // Set sound value
            sound.Play(); // Play sound
            other.gameObject.SetActive(false);
            count = -1; // Prevents the player from winning the game
            winText.text = "You Lose!";
            countText.enabled = false;
        }
    }
}
