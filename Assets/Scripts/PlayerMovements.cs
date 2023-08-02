using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    private bool alive = true;
    private float speed = 10;
    public Rigidbody rb;

    private float horizontalInput;
    private float horizontalMultiplier = 2;
    private float touchSensitivity = 0.01f; // Adjust this to increase/decrease sensitivity


    private void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }
        
        Vector3 forwardMove = transform.forward * (speed * Time.fixedDeltaTime);
        Vector3 horizontalMove = transform.right * (horizontalInput * speed * horizontalMultiplier * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                #if UNITY_WEBGL && !UNITY_EDITOR
                    float inversionFactor = -1f;
                #else
                    float inversionFactor = 1f;
                #endif
                
                horizontalInput = touch.deltaPosition.x * touchSensitivity * inversionFactor;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                horizontalInput = 0;
            }

            horizontalInput = Mathf.Clamp(horizontalInput, -1f, 1f);
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        if (transform.position.y <= -10)
        {
            Die();
        }
        
    }

    public void Die()
    {
        alive = false;

        Invoke("Restart", 2);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
