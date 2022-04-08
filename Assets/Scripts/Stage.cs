using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // to see the vars in the inspector

public class Level
{
    [Range(1, 11)]
    public int partCount = 11;
    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName = "New Stage")]

public class Stage : ScriptableObject
{
    public Color stageBackgorundColor = Color.white;
    public Color stageLevelPartColor = Color.white;
    public Color stageBallColor = Color.white;
    public List<Level> levels = new List<Level>();
}
