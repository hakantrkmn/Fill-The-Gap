using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform sendButton;

    public Transform goBackButton;

    public GameObject levelCompletedPanel;
    public Transform toggleButton;

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
        sendButton.DOScale(1, .3f).SetEase(Ease.OutBounce).SetDelay(.3f);
        toggleButton.DOScale(1, .3f).SetEase(Ease.OutBounce).SetDelay(.3f);
        goBackButton.DOScale(0, .3f).SetEase(Ease.OutBounce).SetDelay(.3f);
    }

    private void LevelCompleted()
    {
        levelCompletedPanel.SetActive(true);
    }

    private void SendPuzzle(Transform obj)
    {
        sendButton.DOScale(0, .3f).SetEase(Ease.OutBounce);
        toggleButton.DOScale(0, .3f).SetEase(Ease.OutBounce);
    }

    private void GoBackButtonClicked()
    {
        sendButton.DOScale(1, .3f).SetEase(Ease.OutBounce);
        toggleButton.DOScale(1, .3f).SetEase(Ease.OutBounce);
        goBackButton.DOScale(0, .3f).SetEase(Ease.OutBounce);
    }

    private void OnDisable()
    {
        EventManager.BoxHitThePuzzle += BoxHitThePuzzle;
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
        
        sendButton.DOScale(0, .3f).SetEase(Ease.OutBounce);
        toggleButton.DOScale(0, .3f).SetEase(Ease.OutBounce);
        goBackButton.DOScale(1, .3f).SetEase(Ease.OutBounce);
    }

    // Update is called once per frame
    void Update()
    {
    }
}