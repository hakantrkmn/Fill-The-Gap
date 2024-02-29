using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    Color _firstColor;


    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
    }

    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
    }

    private void ChangeGameState(GameStates obj)
    {
        if (obj== GameStates.CameraRotating)
        {
            ClearBoxColor();
        }
    }

    private void Start()
    {
        _firstColor = meshRenderer.material.color;
    }
    

    public void ColorBoxRed()
    {
        meshRenderer.material.color = Color.Lerp(meshRenderer.material.color,Color.red, Time.deltaTime/.6f);

    }

    public void ClearBoxColor()
    {
        meshRenderer.material.color = _firstColor;

    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PuzzleCube>())
        {
            EventManager.BoxHitThePuzzle();
        }
    }
}