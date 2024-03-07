using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CanvasGroup startScreen;
    public CanvasGroup buildScreen;
    public CanvasGroup puzzleInspectScreen;
    public CanvasGroup levelCompletedScreen;


    private void OnEnable()
    {
        EventManager.BoxHitThePuzzle += BoxHitThePuzzle;
        EventManager.LevelCompleted += LevelCompleted;
        EventManager.SendPuzzle += SendPuzzle;
        EventManager.ChangeCameraToPuzzle += ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked += GoBackButtonClicked;
    }

    private void BoxHitThePuzzle()
    {
        puzzleInspectScreen.Toggle();
        buildScreen.Toggle();
    }

    private void LevelCompleted()
    {
        levelCompletedScreen.Toggle();
    }

    private void SendPuzzle(Transform obj)
    {
        buildScreen.Toggle();
    }

    private void GoBackButtonClicked()
    {
        buildScreen.Toggle();
        puzzleInspectScreen.Toggle();
    }

    private void OnDisable()
    {
        EventManager.BoxHitThePuzzle -= BoxHitThePuzzle;
        EventManager.LevelCompleted -= LevelCompleted;
        EventManager.SendPuzzle -= SendPuzzle;
        EventManager.ChangeCameraToPuzzle -= ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked -= GoBackButtonClicked;
    }

    public void NextLevel()
    {
        var index = EventManager.GetLevelData().currentLevelIndex;
        SceneManager.LoadScene(index + 1);
    }

    private void ChangeCameraToPuzzle()
    {
        buildScreen.Toggle();
        puzzleInspectScreen.Toggle();
    }

    public void TutorialShowed()
    {
        buildScreen.Toggle();
        var data = EventManager.GetLevelData();
        data.firstPlay = false;
        data.Save();
    }
    private void Start()
    {
        var data = EventManager.GetLevelData();
        if (data.firstPlay)
        {
            startScreen.Toggle();
            buildScreen.Toggle();
        }
        else
        {
            startScreen.gameObject.SetActive(false);
        }


    }
}