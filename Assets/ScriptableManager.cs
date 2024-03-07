using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    public LevelData levelData;


    private void OnEnable()
    {
        EventManager.GetLevelData += () => levelData;
    }

    private void OnDisable()
    {
        EventManager.GetLevelData -= () => levelData;
    }
}
