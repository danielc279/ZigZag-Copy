using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public TextMeshProUGUI[] scoreTexts;

    public TextMeshProUGUI highscoreUI;

    public TextMeshProUGUI scoreUI;

    public TextMeshProUGUI bestScoreStartUI;

    public TextMeshProUGUI timesPlayedUI;

    public Animator gameOverAnim;

    public Animator gameStartReverseAnim;

    public Animator gameOverReverseAnim;

    public Image background;

    public PlayerScript myPlayerScript;

    public bool firstTap = false;
    private int gamesplayedcounter;

    // Start is called before the first frame update
    void Start()
    {

        myPlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        bestScoreStartUI.SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
        gamesplayedcounter = PlayerPrefs.GetInt("TimesPlayed", 0);
        timesPlayedUI.SetText(gamesplayedcounter+"");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");
        scoreUI.gameObject.SetActive(false);
        scoreTexts[1].SetText(myPlayerScript.score.ToString());

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

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

        scoreTexts[3].SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
    }

    public void FirstTap()
    {
        gameStartReverseAnim.SetTrigger("FirstTap");
        scoreUI.gameObject.SetActive(true);
        gamesplayedcounter++;
        PlayerPrefs.SetInt("TimesPlayed", gamesplayedcounter);
        firstTap = true;
    }

    public void Retry()
    {
        gameOverReverseAnim.SetTrigger("Retry");
        StartCoroutine(RetryEvent());
    }

    IEnumerator RetryEvent()
    {
       yield return new WaitForSeconds(0.25f);
       Application.LoadLevel(Application.loadedLevel);
       bestScoreStartUI.SetText(PlayerPrefs.GetInt("BestScore", 0).ToString());
       timesPlayedUI.SetText(gamesplayedcounter+"");
    }
}
