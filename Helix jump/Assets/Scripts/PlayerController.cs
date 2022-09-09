using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public float force;
    private bool ignoreNextcollision;

    public int perfectPass = 0;
    public bool isSuperSpeedActive;

    private Vector3 startPos;
    void Awake()
    {
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Ball touched");

        if (ignoreNextcollision)
            return;
        //destroying platform when you pass 3 platforms in a row
        if (isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<Obstacle>())
            {
                Destroy(collision.transform.parent.gameObject);
                Debug.Log("destroying platforms");
            }
        }
        else
        {
            // to check if the ball touches the obstacle and makes it to restart
            Obstacle obstacletwo = collision.transform.GetComponent<Obstacle>();  //obstacletwo is just an instance of obstacle 
            if (obstacletwo)
                obstacletwo.ObstacleHit();
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);

        ignoreNextcollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;

    }

    public void Update()
    {
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision()
    {
        ignoreNextcollision = false;

    }

    public void ResetBall()
    {
        transform.position = startPos;
    }
}


