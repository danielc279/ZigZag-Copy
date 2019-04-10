using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleScript : MonoBehaviour
{

    // A reference to the particly system.
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        // Calls the particle to play.
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // When the particle script is no longer active delete it from the game objects.
        if (!ps.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
