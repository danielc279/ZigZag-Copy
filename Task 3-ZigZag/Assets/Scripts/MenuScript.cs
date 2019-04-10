using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{

    // An array containing the 4 texts shown in the game over screens background.
    public TextMeshProUGUI[] scoreTexts;

    // The high score text that appears only if you beat your previous score.
    public TextMeshProUGUI highscoreUI;

    // The score that appears in the top right of the screen.
    public TextMeshProUGUI scoreUI;

    // The best score text that appears in the start of the game.
    public TextMeshProUGUI bestScoreStartUI;

    // The times played text that appears in the start of the game.
    public TextMeshProUGUI timesPlayedUI;

    // A reference to the game over animation.
    public Animator gameOverAnim;

    // A reference to the game start reverse animation.
    public Animator gameStartReverseAnim;

    // A reference to the game over reverse animation.
    public Animator gameOverReverseAnim;

    // The grey background that appears in the game over menu.
    public Image background;

    /// A reference to the player script on this scene.
    public PlayerScript myPlayerScript;

    // Sets first tap variable used to deduct if the tap is the first of the game.
    public bool firstTap = false;

    // The amount of times the game has been played.
    private int gamesplayedcounter;

    // Start is called before the first frame update
    void Start()
    {

        // Look for the player script on the same scene.
        myPlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        
        // Setting the best score and times played test to show in the start menu.
        bestScoreStartUI.SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
        gamesplayedcounter = PlayerPrefs.GetInt("TimesPlayed", 0);
        timesPlayedUI.SetText(gamesplayedcounter+"");
    }

    // Called when the player loses the game.
    public void GameOver()
    {
        // calls the game over enter animation and stores all of the score values to be displayed.
        gameOverAnim.SetTrigger("GameOver");
        scoreUI.gameObject.SetActive(false);
        scoreTexts[1].SetText(myPlayerScript.score.ToString());

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // If the players new score is better than their best, override it.
        if (myPlayerScript.score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", myPlayerScript.score);
            highscoreUI.gameObject.SetActive(true);
            background.color = new Color32(253,68,232,255);
            foreach (TextMeshProUGUI txt in scoreTexts)
            {
                txt.color = new Color32(255,255,255,255);
            }
        }

        // Sets the best score
        scoreTexts[3].SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
    }

    //The function that happens on the first tap.
    public void FirstTap()
    {
        // Calls the game start exit animation to leave the screen.
        gameStartReverseAnim.SetTrigger("FirstTap");
        // Activates the score UI on the top right.
        scoreUI.gameObject.SetActive(true);
        // Increments and saves the games played by 1.
        gamesplayedcounter++;
        PlayerPrefs.SetInt("TimesPlayed", gamesplayedcounter);
        // Sets firstTap to true so it doesnt get called again.
        firstTap = true;
    }

    // Loads the exit animation and calls the coroutine.
    public void Retry()
    {
        gameOverReverseAnim.SetTrigger("Retry");
        StartCoroutine(RetryEvent());
    }

    // After the exit animation finishes it resets the game.
    IEnumerator RetryEvent()
    {
        // Time to be waited.
        yield return new WaitForSeconds(0.25f);
        // Function that resets the level.
        Application.LoadLevel(Application.loadedLevel);
        // Sets the bestscore and times played to be shown on reset.
        bestScoreStartUI.SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
        timesPlayedUI.SetText(gamesplayedcounter+"");
    }
}
