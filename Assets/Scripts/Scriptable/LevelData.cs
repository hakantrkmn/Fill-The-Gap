using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int currentLevelIndex;
    public int maxLevelCount;

    [Button]
    public void Save()
    {
        ES3.Save("level",this);
    }


    public void LevelCompleted()
    {
        currentLevelIndex++;
        if (currentLevelIndex>=maxLevelCount)
        {
            currentLevelIndex = 0;
        }
        Save();
    }
}
