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
        DontDestroyOnLoad(this.gameObject);
    }



    public GameStates gameState;
    public ClickMode clickMode;


    private void OnEnable()
    {
        EventManager.ChangeClickMode += mode => clickMode = mode;
        EventManager.ChangeGameState += state => gameState = state;
    }

    private void OnDisable()
    {
        EventManager.ChangeClickMode -= mode => clickMode = mode;
        EventManager.ChangeGameState -= state => gameState = state;
        
    }
}
