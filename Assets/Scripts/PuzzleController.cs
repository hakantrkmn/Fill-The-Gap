﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public List<GameObject> GapCubes;
    int DoneCubeCount;
    public Transform MovePoint;
    public Transform middlePoint;
    void Start()
    {
        DoneCubeCount = 0;
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            GapCubes.Add(transform.GetChild(1).GetChild(i).gameObject);
        }

        foreach (var cube in GetComponentsInChildren<PuzzleCube>())
        {
            middlePoint.position += cube.transform.position;
        }

        middlePoint.position /= GetComponentsInChildren<PuzzleCube>().Length;
    }

    private void OnEnable()
    {
        EventManager.SendButtonClicked += SendButtonClicked;
        EventManager.PuzzleArrived += CheckCubes;
    }

    private void SendButtonClicked()
    {
        EventManager.SendPuzzle(MovePoint);
    }

    private void OnDisable()
    {
        EventManager.SendButtonClicked -= SendButtonClicked;

        EventManager.PuzzleArrived -= CheckCubes;
    }

    private void CheckCubes()
    {

        EventManager.CheckCubes();

        foreach (var cube in GapCubes)
        {
            if (cube.GetComponent<GapCube>().IsDone)
            {
                DoneCubeCount++;
            }
        }

        if (DoneCubeCount == GapCubes.Count)
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