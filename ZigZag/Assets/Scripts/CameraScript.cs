using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;

    public float pivot;

    private PlayerScript myPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        myPlayerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayerScript.IsPlaying)
        {
        transform.position = new Vector3((Math.Abs(player.transform.position.x) + Math.Abs(player.transform.position.z))/-2 + pivot,
                                        transform.position.y,(Math.Abs(player.transform.position.x) + Math.Abs(player.transform.position.z))/2 - pivot);
        }
    }
}
