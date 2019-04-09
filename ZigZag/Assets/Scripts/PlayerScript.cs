using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    // The speed of the sphere.
    public float speed;

    // The direction of the sphere.
    private Vector3 dir;

    // The particle effect on collision with a pickup.
    public GameObject ps;

    // Whether the player has died or is still playing.
    private bool isDead;

    // The retry button that resets the game.
    public GameObject resetBtn;

    // The score shown in the top right corner.
    public TextMeshProUGUI scoreUI;

    // Layer mask used to detect if anything is being collided with.
    public LayerMask whatIsGround;

    // Whether the player is currently playing the game or not.
    public bool IsPlaying {get; set;}

    // The contactpoint situated at the bottom of the player.
    public Transform contactPoint;

    // The players score.
    public int score { get; private set; }

    /// A reference to the menu script on this scene.
    public MenuScript myMenuScript;

    // Start is called before the first frame update.
    void Start()
    {
        // Sets all variables to their default value.
        dir = Vector3.zero;

        IsPlaying = true;

        isDead = false;

        score = 0; 

        // Look for the menu script on the same scene.
        myMenuScript = GameObject.Find("GameManager").GetComponent<MenuScript>();
    }

    // Update is called once per frame.
    void Update()
    {
        // The moment the player stops touching the ground they lose the game and go to the game over menu.
        if (!IsGrounded() && IsPlaying)
        {
            isDead = true;
            // Stops the camera from moving and tiles from falling.
            IsPlaying = false;
            // Loads the Game Over function and reset button.
            myMenuScript.GameOver();

            resetBtn.SetActive(true);

            if (transform.childCount > 0)
            {
            transform.GetChild(0).transform.parent = null;
            }
        }

        // Triggered when the played clicks and is not dead, therefore when they are playing the game.
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            IsPlaying = true;
            score++; 
            scoreUI.SetText(score.ToString());
            // On the first tap it should remove the Game Start menu.
            if (myMenuScript.firstTap == false)
            {
                myMenuScript.FirstTap();    
            }
            
            // If the player is moving forward it will set the direction to the left, if it is facing any other direction it will set the direction to forward.
            // This is the function that causes it to switch between two directions.
            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }

        }

        // Actually moves the sphere in the set direction at a constant speed.
        float amountToMove = speed * Time.deltaTime;

        transform.Translate(dir * amountToMove);
    }

    // When the player collides with a gem it should gain points, call the particle system and delete the gem.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            Instantiate(ps, transform.position, Quaternion.identity);
            score+=2; 
            scoreUI.SetText(score.ToString());
        }
    }

    // Checks if the player is touching the ground to keep it in the game.
    private bool IsGrounded()
    {

        // Returns an array with all colliders touching the spheres contact point.
        Collider[] colliders = Physics.OverlapSphere(contactPoint.position,.5f, whatIsGround); 

        for (int i = 0; i < colliders.Length; i++)
        {
            // Whilst the contact point is touching another object it will return true.
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
