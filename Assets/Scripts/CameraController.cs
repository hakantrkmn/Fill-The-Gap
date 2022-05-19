using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Puzzle;
    public Transform player;
    public CinemachineVirtualCamera playerCameraNoFocus;
    public CinemachineVirtualCamera playerCameraWithFocus;
    public CinemachineVirtualCamera PuzzleCamera;

    private Vector3 mouseStartPos;
    public float rotateSpeed;
    private bool canRotate=true;

    private void OnEnable()
    {
        EventManager.ChangeCameraToPuzzle += ChangeCameraToPuzzle;
        EventManager.SendButtonClicked += SendButtonClicked;
        EventManager.GoBackButtonClicked += GoBackButtonClicked;
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
        EventManager.SendButtonClicked -= SendButtonClicked;
        EventManager.ChangeCameraToPuzzle -= ChangeCameraToPuzzle;
        EventManager.GoBackButtonClicked -= GoBackButtonClicked;
    }

    private void Start()
    {
        Puzzle = GameObject.FindObjectOfType<PuzzleController>().transform;
        player = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    private void ChangeCameraToPuzzle()
    {
        playerCameraNoFocus.Priority = 0;
        PuzzleCamera.Priority = 10;
    }

    private void Update()
    {
        if (canRotate)
        {
            if (PuzzleCamera.Priority > playerCameraNoFocus.Priority)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mouseStartPos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    var speed = (Input.mousePosition - mouseStartPos).normalized.x * rotateSpeed;
                    RotateAround(PuzzleCamera.transform, Puzzle.position, Quaternion.Euler(0, -speed, 0));
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mouseStartPos = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    var speed = (Input.mousePosition - mouseStartPos).normalized.x * rotateSpeed;
                    RotateAround(playerCameraNoFocus.transform, player.position, Quaternion.Euler(0, -speed, 0));
                }
            }
        }
    }

    static void RotateAround(Transform transform, Vector3 pivotPoint, Quaternion rot)
    {
        transform.position = rot * (transform.position - pivotPoint) + pivotPoint;
        transform.rotation = rot * transform.rotation;
    }
}