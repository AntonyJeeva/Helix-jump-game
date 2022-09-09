using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void ObstacleHit()
    {
        GameManager.singleton.RestartLevel();
    }
}
