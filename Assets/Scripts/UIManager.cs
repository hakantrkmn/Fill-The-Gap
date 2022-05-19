using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button placeButton;
    public Button sendButton;
    public Button destroyButton;

    public Button goBackButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        EventManager.ChangeCameraToPuzzle += ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked += GoBackButtonClicked;
    }

    private void GoBackButtonClicked()
    {
        placeButton.gameObject.SetActive(true);
        sendButton.gameObject.SetActive(true);
        destroyButton.gameObject.SetActive(true);

        goBackButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.ChangeCameraToPuzzle -= ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked -= GoBackButtonClicked;
    }

    private void ChangeCameraToPuzzle()
    {
        
        placeButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        sendButton.gameObject.SetActive(false);
        goBackButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}