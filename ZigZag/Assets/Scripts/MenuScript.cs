using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{

    public TextMeshProUGUI[] scoreTexts;

    public TextMeshProUGUI highscoreUI;

    public Animator gameOverAnim;

    public Image background;

    public PlayerScript myPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");
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
}
