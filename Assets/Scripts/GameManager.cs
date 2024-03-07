using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() 
    { 
        
        if (instance != null && instance != this) 
        { 
            Destroy(this);
            return;
        } 
        instance = this;
    }


    private void Start()
    {
        var data = EventManager.GetLevelData();
        if (ES3.KeyExists("level"))
        {
            ES3.Load("level", data);
        }
        else
        {
            ES3.Save("level",data);
        }
    }


    public GameStates gameState;
    public ClickMode clickMode;


    private void OnEnable()
    {
        EventManager.LevelCompleted += LevelCompleted;
        EventManager.ChangeClickMode += mode => clickMode = mode;
        EventManager.ChangeGameState += state => gameState = state;
    }

    private void LevelCompleted()
    {
        var data = EventManager.GetLevelData();
        data.LevelCompleted();
        gameState = GameStates.LevelCompleted;
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= LevelCompleted;
        EventManager.ChangeClickMode -= mode => clickMode = mode;
        EventManager.ChangeGameState -= state => gameState = state;
        
    }
}
