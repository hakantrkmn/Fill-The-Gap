using System;
using UnityEngine;

public static class EventManager
{
    public static Action SendButtonClicked;
    public static Action GoBackButtonClicked;
    public static Action PuzzleArrived;
    public static Action CheckCubes;
    public static Action BoxHitThePuzzle;
    public static Action<Transform> SendPuzzle;
    public static Action ChangeCameraToPuzzle;
    public static Action<GameStates> ChangeGameState;

    public static Action<ClickMode> ChangeClickMode;

    
    
    public static Action<BoxController,Vector3> BoxPlaced;
    public static Action<BoxController,BoxController> BoxDestroyed;

    
    
    
}