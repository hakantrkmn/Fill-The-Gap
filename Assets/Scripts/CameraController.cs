using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PuzzleController Puzzle;
    Transform player;
    public CinemachineVirtualCamera playerCameraNoFocus;
    public CinemachineVirtualCamera playerCameraWithFocus;
    public CinemachineVirtualCamera PuzzleCamera;

    private Vector3 mouseStartPos;
    private bool canRotate = true;
    [Range(3,10)]
    public float rotationSpeed;
    Transform lastPlacedBox;

    private void OnEnable()
    {
        EventManager.BoxDestroyed += BoxDestroyed;
        EventManager.BoxPlaced += BoxPlaced;
        EventManager.ChangeCameraToPuzzle += ChangeCameraToPuzzle;
        EventManager.SendButtonClicked += SendButtonClicked;
        EventManager.GoBackButtonClicked += GoBackButtonClicked;
    }

    private void BoxDestroyed(BoxController arg1, BoxController arg2)
    {

        if (lastPlacedBox == arg1)
        {
            lastPlacedBox = arg2.transform;
            playerCameraNoFocus.transform.DOLookAt(lastPlacedBox.position, .5f);
            playerCameraNoFocus.Follow = lastPlacedBox;

        }
    }

    private void BoxPlaced(BoxController obj, Vector3 dir)
    {
        playerCameraNoFocus.transform.DOLookAt(obj.transform.position, .5f);
        lastPlacedBox = obj.transform;
        playerCameraNoFocus.Follow = lastPlacedBox;
    }

    private void SendButtonClicked()
    {
        canRotate = false;
        playerCameraNoFocus.Priority = 0;
        playerCameraWithFocus.Priority = 10;
    }

    private void GoBackButtonClicked()
    {
        playerCameraNoFocus.Priority = 10;
        PuzzleCamera.Priority = 0;
    }

    private void OnDisable()
    {
        EventManager.BoxDestroyed -= BoxDestroyed;
        EventManager.BoxPlaced -= BoxPlaced;
        EventManager.SendButtonClicked -= SendButtonClicked;
        EventManager.ChangeCameraToPuzzle -= ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked -= GoBackButtonClicked;
    }

    private void Start()
    {
        Puzzle = FindObjectOfType<PuzzleController>();
        player = FindObjectOfType<PlayerController>().transform;
        lastPlacedBox = player.GetComponent<PlayerController>().startBox.transform;
        playerCameraNoFocus.transform.DOLookAt(lastPlacedBox.position, .5f);
    }

    private void ChangeCameraToPuzzle()
    {
        PuzzleCamera.transform.DOLookAt(Puzzle.middlePoint.position, .5f);
        playerCameraNoFocus.Priority = 0;
        PuzzleCamera.Priority = 10;
    }

    private void Update()
    {
        if (canRotate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseStartPos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                if (Vector3.Distance(mouseStartPos, Input.mousePosition) > 30)
                {
                    if (PuzzleCamera.Priority > playerCameraNoFocus.Priority)
                    {
                        EventManager.ChangeGameState(GameStates.CameraRotating);


                        PuzzleCamera.transform.RotateAround(Puzzle.middlePoint.position, Vector3.up,
                            Input.GetAxis("Mouse X") * rotationSpeed);
                        PuzzleCamera.transform.RotateAround(Puzzle.middlePoint.position, PuzzleCamera.transform.right,
                            Input.GetAxis("Mouse Y") * -rotationSpeed);
                    }
                    else
                    {
                        EventManager.ChangeGameState(GameStates.CameraRotating);
                        playerCameraNoFocus.transform.RotateAround(lastPlacedBox.position, Vector3.up,
                            Input.GetAxis("Mouse X") * rotationSpeed);
                        playerCameraNoFocus.transform.RotateAround(lastPlacedBox.position,
                            playerCameraNoFocus.transform.right, Input.GetAxis("Mouse Y") * -rotationSpeed);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EventManager.ChangeGameState(GameStates.PlaceBox);
            }
        }
    }

  
}