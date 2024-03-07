using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        var index = EventManager.GetLevelData().currentLevelIndex;
        SceneManager.LoadScene(index + 1);
    }

   
}
