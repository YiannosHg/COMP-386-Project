using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed; // The speed the ball moves
    public float jumpForce; // The speed the ball jumps
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

    private void FixedUpdate()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(xInput, 0.0f, zInput) * moveSpeed);

        // Jump if ball touches the ground
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Collision detection to deactivate the PickUp object we touched and make sound
    // and lose the game if we touched a DoNotPickUp object and make sound
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
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
        else if (other.gameObject.CompareTag("DoNotPickUp"))
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
