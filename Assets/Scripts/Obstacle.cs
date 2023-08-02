using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerMovements playerMovements;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovements = GameObject.FindObjectOfType<PlayerMovements>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerMovements.Die();
        }
    }
}
