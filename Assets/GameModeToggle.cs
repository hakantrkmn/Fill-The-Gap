using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class GameModeToggle : MonoBehaviour
{

    public ClickMode clickMode;

    private void Start()
    {
        ChangeClickMode();
    }

    void ChangeClickMode()
    {
        clickMode = GetComponent<SegmentedControl>().selectedSegmentIndex == 0
            ? clickMode = ClickMode.Destroy
            : clickMode = ClickMode.Place;

        EventManager.ChangeClickMode(clickMode);
    }
}
