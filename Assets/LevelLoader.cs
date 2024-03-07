using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Start()
    {
        var data = EventManager.GetLevelData();
        if (ES3.KeyExists("level"))
        {
            data = ES3.Load<LevelData>("level");
        }
        else
        {
            ES3.Save("level",data);
        }

        var index = data.currentLevelIndex;
        SceneManager.LoadScene(index + 1);
    }

   
}
