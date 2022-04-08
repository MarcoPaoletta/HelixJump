using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float jumpForce = 3f;

    private bool ignoreNextCollision;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // avoid to collide 2 times
        if(ignoreNextCollision)
        {
            return;
        }
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>(); // obtain the script of the death part

        if(deathPart)
        {
            GameManager.singleton.RestartLevel();
        }
        // add force
        rb.velocity = Vector3.zero; // set the current velocity to 0 to avoid errors
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        ignoreNextCollision = true;
        Invoke("AllowNextCollision", 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBallPosition()
    {
        transform.position = startPosition;
    }
}
