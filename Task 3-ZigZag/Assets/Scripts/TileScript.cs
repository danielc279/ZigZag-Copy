using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	// Tiles will wait for the delay to pass before they being falling.
	private float fallDelay = 1.5f;

	// A reference to the player.
	public GameObject player;

	// Store the rigidbody component in memory for easier access.
	private Rigidbody myRB;

	// A reference to the player script on this scene.
	private PlayerScript myPlayerScript;
	// Use this for initialization
	void Start () {
		// Look for the rigidbody on the same scene.
		myRB = gameObject.GetComponent<Rigidbody>();
		// Link the player to the player tag.
		player = GameObject.Find("Player");
		// Look for the playerscript on the same scene.
		myPlayerScript = player.GetComponent<PlayerScript>();
	}
	
	// When a player exits the collider of a tile, a new tile is spawn and the corouting is started.
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			TileManager.Instance.SpawnTile();
			// Corouting causes the tile to wait for a set time and then fall down.
			StartCoroutine(FallDown());
		}
	}

	// Coroutine that handles the falling and deleting of passed tiles.
	IEnumerator FallDown()
	{
		// The time to wait before the tile falls.
		yield return new WaitForSeconds(fallDelay);
		if (myPlayerScript.IsPlaying){
			//sets the tile's kinematic to false to make it fall.
			myRB.isKinematic = false;
			// The time to wait before the tile is deleted.
			yield return new WaitForSeconds(2);
			switch (gameObject.name)
			{
				//In each case it pushes the game object to be deleted onto the stack.
				//In each case it sets the kinematic back to true so it wont fall when spawned again.
				//In each case it deletes the tile.
				case "LeftTile":
					TileManager.Instance.LeftTiles.Push(gameObject);
					myRB.isKinematic = true;
					gameObject.SetActive(false);
					break;

				case "TopTile":
					TileManager.Instance.TopTiles.Push(gameObject);
					myRB.isKinematic = true;
					gameObject.SetActive(false);
					break;
				case "StartTile":
					gameObject.SetActive(false);
					break;	
			}
		}

	}
}
