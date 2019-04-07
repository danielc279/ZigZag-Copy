using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    public float speed;

    private Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public Animator gameOverAnim;

    public GameObject resetBtn;

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highscoreUI;

    public TextMeshProUGUI[] scoreTexts;
    public Image background;
    public int score { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isDead = false;
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            score++; 
            scoreUI.SetText(score.ToString());

            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }

        }

        float amountToMove = speed * Time.deltaTime;

        transform.Translate(dir * amountToMove);
    }

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

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            RaycastHit hit;

            Ray downRay = new Ray(transform.position, -Vector3.up);

            if (!Physics.Raycast(downRay, out hit))
            {
                isDead = true;
                GameOver();
                resetBtn.SetActive(true);
                if (transform.childCount > 0)
                {
                transform.GetChild(0).transform.parent = null;
                }

            }
        }
    }

    private void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");
        scoreTexts[1].SetText(score.ToString());

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
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
