using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GameModeToggle : MonoBehaviour
{
    public Image image;
    public ClickMode clickMode;
    public TextMeshProUGUI text;

    public Color destroyColor;
    public Color placeColor;
    private void Start()
    {
        ChangeClickMode();
    }

    public void ChangeClickMode()
    {
        if (clickMode == ClickMode.Destroy)
        {
            clickMode = ClickMode.Place;
            image.DOColor(placeColor, .3f);
        }
        else
        {
            clickMode = ClickMode.Destroy;
            image.DOColor(destroyColor, .3f);
        }
        text.text = clickMode.ToString();
        EventManager.ChangeClickMode(clickMode);
    }
}
