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



    public GameObject resetBtn;

    public TextMeshProUGUI scoreUI;

    public LayerMask whatIsGround;

    public bool IsPlaying {get; set;}

    public Transform contactPoint;


    public int score { get; private set; }

    public MenuScript myMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        IsPlaying = true;
        myMenuScript = GameObject.Find("GameManager").GetComponent<MenuScript>();
        score = 0;
        isDead = false;
        dir = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsGrounded() && IsPlaying)
        {
            isDead = true;
            IsPlaying = false;
            myMenuScript.GameOver();

            resetBtn.SetActive(true);

            if (transform.childCount > 0)
            {
            transform.GetChild(0).transform.parent = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            IsPlaying = true;
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

    private bool IsGrounded()
    {
       Collider[] colliders = Physics.OverlapSphere(contactPoint.position,.5f, whatIsGround); 

       for (int i = 0; i < colliders.Length; i++)
       {
           if (colliders[i].gameObject != gameObject)
           {
               return true;
           }
       }

       return false;
    }
}
