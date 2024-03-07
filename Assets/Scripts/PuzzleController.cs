using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public Transform destinationCube;
    List<GameObject> gapCubes = new List<GameObject>();
    int DoneCubeCount;
    public Transform MovePoint;
    public Transform middlePoint;
    void Start()
    {
        DoneCubeCount = 0;

        foreach (var cube in GetComponentsInChildren<GapCube>())
        {
            gapCubes.Add(cube.gameObject);
        }
        
        foreach (var cube in GetComponentsInChildren<PuzzleCube>())
        {
            middlePoint.position += cube.transform.position;
        }

        middlePoint.position /= GetComponentsInChildren<PuzzleCube>().Length;
        
        CreateDestinationCube();
    }

    void CreateDestinationCube()
    {
        var pos = new Vector3(EventManager.GetStartCubePos().x, EventManager.GetStartCubePos().y, MovePoint.position.z);
        destinationCube.position = pos;
    }

    private void OnEnable()
    {
        EventManager.LevelCompleted += LevelCompleted;
        EventManager.SendButtonClicked += SendButtonClicked;
        EventManager.PuzzleArrived += CheckCubes;
    }

    private void LevelCompleted()
    {
        destinationCube.gameObject.SetActive(false);
    }

    private void SendButtonClicked()
    {
        EventManager.SendPuzzle(MovePoint);
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= LevelCompleted;
        EventManager.SendButtonClicked -= SendButtonClicked;
        EventManager.PuzzleArrived -= CheckCubes;
    }

    private void CheckCubes()
    {

        EventManager.CheckCubes();

        foreach (var cube in gapCubes)
        {
            if (cube.GetComponent<GapCube>().IsDone)
            {
                DoneCubeCount++;
            }
        }

        if (DoneCubeCount == gapCubes.Count)
        {
            Debug.Log("Level Done");
            EventManager.LevelCompleted();
        }
        else
        {
            Debug.Log("Level Failed");
            EventManager.BoxHitThePuzzle();
            DoneCubeCount = 0;

        }
    }


}