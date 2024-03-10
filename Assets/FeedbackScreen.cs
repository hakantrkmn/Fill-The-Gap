using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FeedbackScreen : MonoBehaviour
{
    public Transform outOfCubeText;
    public Transform lotsOfCubeText;

    private void OnEnable()
    {
        EventManager.LotsOfCube += LotsOfCube;
    }

    private void LotsOfCube()
    {
        DOTween.Complete("lotsOfCube");
        lotsOfCubeText.localScale = Vector3.one;
        lotsOfCubeText.DOShakeRotation(.5f,45).SetId("lotsOfCube").OnComplete(() =>
        {
            lotsOfCubeText.localScale = Vector3.zero;
        });
    }

    private void OutOfCube(Vector3 pos)
    {
        DOTween.Complete("OutOfCube");
        outOfCubeText.position = pos;
        outOfCubeText.localScale = Vector3.one;
        outOfCubeText.DOShakeRotation(.5f,45).SetId("OutOfCube").OnComplete(() =>
        {
            outOfCubeText.localScale = Vector3.zero;
        });
        
    }

    private void OnDisable()
    {
        EventManager.LotsOfCube -= LotsOfCube;
    }

    
}
