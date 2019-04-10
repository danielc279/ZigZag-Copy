using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour 
{
	// The last tile that we spawned, this is used as a reference for the next tile we spawn.
	public GameObject currentTile;

	// A list of the tiles that we can spawn.
	public GameObject[] tilePrefabs;

	// A reference to the menu script on this scene.
	public MenuScript myMenuScript;
	
	// Used to keep track of the position of the tile on the screen. Set to one as that is always the position of the first spawned tile.
	int spawnIndex = 1;

	// A stack that contains all the left tiles. This is used for recycling.
	private Stack<GameObject> leftTiles = new Stack<GameObject>();

	// A stack that contains all the top tiles. This is used for recycling.
	private Stack<GameObject> topTiles = new Stack<GameObject>();

	// Used to allow the access to the left tile stack.
	public Stack<GameObject> LeftTiles
	{
		get { return leftTiles; }
		set { leftTiles = value; }
	}

	// Used to allow the access to the top tile stack.
	public Stack<GameObject> TopTiles
	{
		get { return topTiles; }
		set { topTiles = value; }
	}

	// Creating a singleton instance for the TileManager	
	private static TileManager instance;

	// Used to allow access to the singleton instance
	public static TileManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<TileManager>();
			}
			return instance;
		}
	}

	// Actions taken at the start of the game.
	void Start () 
	{	
		// look for the menu script on the same scene
		myMenuScript = GameObject.Find("GameManager").GetComponent<MenuScript>();
		// Pushing 100 tiles into both the left tile stack and top tile stack.	
		CreateTiles(100);
		// Loads 50 tiles onto the map the moment the game starts.
		for (int i = 0; i < 50; i++)
		{
		SpawnTile();
		}
	}

	// Creates tiles for both left tile stack and top tile stack based on the inputted amount.
	public void CreateTiles(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			leftTiles.Push(Instantiate(tilePrefabs[0]));
			topTiles.Push(Instantiate(tilePrefabs[1]));
			leftTiles.Peek().name = "LeftTile";
			leftTiles.Peek().SetActive(false);
			topTiles.Peek().name = "TopTile";			
			topTiles.Peek().SetActive(false);
		}
	}

	// Generates a random index of 0 or 1. The result will take a tile either from the top tile stack
	// or left tile stack and spawn it onto the map adjacent to the then current tile.
	public void SpawnTile()
	{
		// If the stack being taken from is empty generate 10 tiles into the stack.
		if (leftTiles.Count == 0 || topTiles.Count == 0)
		{
			CreateTiles(10);
		}

		// Creating the random index to be used for tile spawning.
		int randomIndex = Random.Range(0,2);

		// The first two if statements are extremes that occur when the current tile is on the edge of the screen.
		// They force the next tile to spawn to be towards the centre rather than randomly chosen.
		if(spawnIndex > 4){
			GameObject tmp = leftTiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(0).position;
			currentTile = tmp;

			spawnIndex--;
		}
		else if(spawnIndex < -4){
			GameObject tmp = topTiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(1).position;
			currentTile = tmp;

			spawnIndex++;
		}

		// If the random index is 0 it spawns a left tile whilst if it is 1 it spawns a top tile.
		else if (randomIndex == 0)
		{
			GameObject tmp = leftTiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
			currentTile = tmp;

			spawnIndex--;
		}
		else if (randomIndex == 1)
		{
			GameObject tmp = topTiles.Pop();
			tmp.SetActive(true);
			tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(randomIndex).position;
			currentTile = tmp;

			spawnIndex++;
		}

		// Used to create a chance that a gem can appear on the tile.
		int spawnPickup = Random.Range(0, 10);

		if (spawnPickup == 0)
		{
			currentTile.transform.GetChild(1).gameObject.SetActive(true);
		}
	}

	// Resets the game back to the default start.
	public void ResetGame()
	{
		myMenuScript.Retry();
	}
}
