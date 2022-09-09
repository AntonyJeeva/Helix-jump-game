using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text textScore;

    [SerializeField] private Text textBest;

   
    void Start()
    {
        
    }

    void Update()
    {
        textBest.text = "Best: " + GameManager.singleton.bestScore;
        textScore.text = "Score: " + GameManager.singleton.score;

    }  
}
