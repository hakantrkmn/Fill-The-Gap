using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public Transform destinationCube;
    List<GameObject> gapCubes = new List<GameObject>();
    private int placedCubeAmount;
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
        EventManager.PuzzleIsReady();
    }

    void CreateDestinationCube()
    {
        var pos = new Vector3(EventManager.GetStartCubePos().x, EventManager.GetStartCubePos().y, MovePoint.position.z);
        destinationCube.position = pos;
    }

    private void OnEnable()
    {
        EventManager.BoxDestroyed += (controller, boxController) => placedCubeAmount--; 
        EventManager.BoxPlaced += (controller, vector3) => placedCubeAmount++; 
        EventManager.HaveExtraCube += HaveExtraCube;
        EventManager.GetMaxCubeAmount += () => gapCubes.Count;
        EventManager.LevelCompleted += LevelCompleted;
        EventManager.SendButtonClicked += SendButtonClicked;
        EventManager.PuzzleArrived += CheckCubes;
    }

    private bool HaveExtraCube()
    {
        if (placedCubeAmount + 1> gapCubes.Count)
        {
            return true;
        }

        return false;
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
        EventManager.BoxDestroyed -= (controller, boxController) => placedCubeAmount--;
        EventManager.BoxPlaced -= (controller, vector3) => placedCubeAmount++;
        EventManager.HaveExtraCube -= HaveExtraCube;
        EventManager.GetMaxCubeAmount -= () => gapCubes.Count;
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

    
    List<Vector3> spawnPoints;
    public GameObject gapPrefab;
    public Transform gapParent;
    [Button]
    void CreateGapCubes()
    {
        spawnPoints = new List<Vector3>();
        spawnPoints.Clear();
        foreach (var cube in GetComponentsInChildren<PuzzleCube>())
        {
            cube.transform.position = new Vector3(Mathf.RoundToInt(cube.transform.position.x) , Mathf.RoundToInt(cube.transform.position.y),
                Mathf.RoundToInt(cube.transform.position.z));
        }
        var puzzleCubes = new List<PuzzleCube>();
        foreach (var cube in GetComponentsInChildren<GapCube>())
        {
            DestroyImmediate(cube.gameObject);
        }
        
        foreach (var cube in GetComponentsInChildren<PuzzleCube>())
        {
            puzzleCubes.Add(cube);
            cube.GetPoints();
            foreach (var cu in cube.points)
            {
                var temp = new Vector3(Mathf.FloorToInt(cu.x), Mathf.FloorToInt(cu.y), Mathf.FloorToInt(cu.z));
                if (!spawnPoints.Contains(temp))
                {
                    spawnPoints.Add(cu);
                }
            }
        }

        foreach (var cube in puzzleCubes)
        {
            if (spawnPoints.Contains(cube.transform.position))
            {
                spawnPoints.Remove(cube.transform.position);
            }
        }
        
        
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var temp = new Vector3(Mathf.FloorToInt(spawnPoints[i].x), Mathf.FloorToInt(spawnPoints[i].y), Mathf.FloorToInt(spawnPoints[i].z));
            spawnPoints[i] = temp;
        }

        spawnPoints = spawnPoints.Distinct().ToList();


        foreach (var point in spawnPoints)
        {
            var temp = new Vector3(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), Mathf.FloorToInt(point.z));
            Instantiate(gapPrefab, temp, quaternion.identity, gapParent);
        }
        
        
        
    }
    
    

}