using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Platform
{    
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int obstaclePartCount = 1;
}


[CreateAssetMenu (fileName = "New Stage")]
public class Stage : ScriptableObject
{

    public Color stageBackgroundColor = Color.white;
    public Color stagePlatformColor = Color.white;
    public Color stageBallColor = Color.white;

    public List<Platform> _platforms = new List<Platform>();

}
