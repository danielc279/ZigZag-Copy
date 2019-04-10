using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // A reference to the player.
    GameObject player;

    // Creating the variable pivot to be used in the camera movement calculation.
    public float pivot;

    // A reference to the player script on this scene.
    private PlayerScript myPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Link the player to the player tag.
		player = GameObject.Find("Player");
		// Look for the playerscript on the same scene.
		myPlayerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Only moves the camera when the player is playing the game.
        if (myPlayerScript.IsPlaying)
        {
        // Takes the average of the x position and z position to keep the camera in the center of the screen no matter where the player moves.
        // Only follows the player in this axis.
        transform.position = new Vector3((Math.Abs(player.transform.position.x) + Math.Abs(player.transform.position.z))/-2 + pivot,
                                        transform.position.y,(Math.Abs(player.transform.position.x) + Math.Abs(player.transform.position.z))/2 - pivot);
        }
    }
}
