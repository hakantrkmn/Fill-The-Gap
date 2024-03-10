using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleInspectScreen : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public int maxCubeAmount;

    public int placedCubeAmount = 1;

    private void OnEnable()
    {
        EventManager.PuzzleIsReady += PuzzleIsReady;
        EventManager.BoxPlaced += BoxPlaced;
        EventManager.BoxDestroyed += BoxDestroyed;
    }

    private void BoxDestroyed(BoxController arg1, BoxController arg2)
    {
        placedCubeAmount--;
        PuzzleIsReady();
    }

    private void BoxPlaced(BoxController arg1, Vector3 arg2)
    {
        placedCubeAmount++;
        PuzzleIsReady();
    }

    private void OnDisable()
    {
        EventManager.PuzzleIsReady -= PuzzleIsReady;
        EventManager.BoxPlaced -= BoxPlaced;
        EventManager.BoxDestroyed -= BoxDestroyed;
    }

    private void PuzzleIsReady()
    {
        maxCubeAmount = EventManager.GetMaxCubeAmount();
        countText.text = placedCubeAmount + "/" + maxCubeAmount + " Cubes";
    }
}
