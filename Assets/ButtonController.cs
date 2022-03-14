﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public ButtonTypes type;

    public void ButtonClicked()
    {
        switch (type)
        {
            case ButtonTypes.SendThePuzzle:
                EventManager.SendButtonClicked();
                break;
            case ButtonTypes.Place:
                EventManager.PlaceButtonClicked();
                break;
                
        }
    }
}