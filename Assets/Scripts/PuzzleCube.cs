using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCube : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (GameManager.instance.gameState == GameStates.PlaceBox)
        {
            EventManager.ChangeGameState(GameStates.OnPuzzle);
            EventManager.ChangeCameraToPuzzle();

        }
    }
}
