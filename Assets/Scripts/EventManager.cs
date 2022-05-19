using System;
using UnityEngine;

public static class EventManager
{
    public static Action UpdateNeighbours;
    public static Action SendButtonClicked;
    public static Action PlaceButtonClicked;
    public static Action RotateLeftButtonClicked;
    public static Action RotateRightButtonClicked;
    public static Action GoBackButtonClicked;
    public static Action DestroyButtonClicked;
    public static Action<GameObject> BoxDestroyed;
    public static Action PuzzleArrived;
    public static Action CheckCubes;
    public static Action BoxHitThePuzzle;
    public static Action<Transform> SendPuzzle;
    public static Action<GameObject> UpdateChoosenBox;
    public static Action ChangeCameraToPuzzle;
    
    
    
    
    
    
    
}