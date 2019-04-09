using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {

	private float fallDelay = 1.5f;

	GameObject player;

	private Rigidbody myRB;

	private PlayerScript myPlayerScript;
	// Use this for initialization
	void Start () {
		myRB = gameObject.GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		myPlayerScript = player.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			TileManager.Instance.SpawnTile();
			StartCoroutine(FallDown());
		}
	}

	IEnumerator FallDown()
	{
		yield return new WaitForSeconds(fallDelay);
		if (myPlayerScript.IsPlaying){
			myRB.isKinematic = false;
			yield return new WaitForSeconds(2);
			switch (gameObject.name)
			{
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
