using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform sendButton;

    public Transform goBackButton;

    public Transform toggleButton;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.SendPuzzle += SendPuzzle;
        EventManager.ChangeCameraToPuzzle += ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked += GoBackButtonClicked;
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
        EventManager.SendPuzzle -= SendPuzzle;
        EventManager.ChangeCameraToPuzzle -= ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked -= GoBackButtonClicked;
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