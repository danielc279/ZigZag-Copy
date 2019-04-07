using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float speed;

    private Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public GameObject resetBtn;

    private int score = 0;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            score++; 
            scoreText.text = score.ToString();

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
            score+=3; 
            scoreText.text = score.ToString();
        }
    }

    void OnTriggerExit(Collider other)
    {
        RaycastHit hit;

        Ray downRay = new Ray(transform.position, -Vector3.up);

        if (!Physics.Raycast(downRay, out hit))
        {
            isDead = true;
            resetBtn.SetActive(true);
            if (transform.childCount > 0)
            {
               transform.GetChild(0).transform.parent = null;
            }

        }
    }
}
