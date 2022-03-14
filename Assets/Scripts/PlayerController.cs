using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject Box;
    public GameObject ChoosenBox;
    public GameObject TransparentBox;
    public float TransParentBoxMoveTime;

    Tween tw;

    private void OnEnable()
    {
        EventManager.BoxHitThePuzzle += PuzzleHitTheBox;
        EventManager.SendPuzzle += SendPuzzle;
        EventManager.UpdateChoosenBox += UpdateChoosenBox;
        EventManager.PlaceButtonClicked += PlaceBox;
    }

    private void OnDisable()
    {
        EventManager.BoxHitThePuzzle -= PuzzleHitTheBox;

        EventManager.PlaceButtonClicked -= PlaceBox;

        EventManager.SendPuzzle -= SendPuzzle;
        EventManager.UpdateChoosenBox -= UpdateChoosenBox;
    }

    private void PuzzleHitTheBox()
    {
        tw.Kill();
        Debug.Log("Level Failed");
    }


    private void UpdateChoosenBox(GameObject obj)
    {
        EventManager.UpdateNeighbours();
        ChoosenBox = obj;
        DOTween.Kill("move1");
        TransBoxMove(ChoosenBox);
    }

    void Start()
    {
        TransparentBox = Instantiate(TransparentBox, transform.position, Quaternion.identity);
        TransBoxMove(ChoosenBox);
    }


    private void SendPuzzle(Transform MovePoint)
    {
        TransparentBox.GetComponent<MeshRenderer>().enabled = false;
        DOTween.Kill("move1");
        tw = transform.DOMoveZ(MovePoint.position.z, 4).OnComplete(() => { EventManager.PuzzleArrived(); });
    }


    public void PlaceBox()
    {
        var temp = Instantiate(Box, TransparentBox.transform.position, Quaternion.identity, transform);
        EventManager.UpdateNeighbours();
        ChoosenBox = temp;
        DOTween.Kill("move1");
        TransBoxMove(ChoosenBox);
    }


    public void TransBoxMove(GameObject Box)
    {
        Sequence move = DOTween.Sequence().SetId("move1");
        if (Box.GetComponent<BoxController>().LeftX == null)
        {
            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(1, 0, 0);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        if (Box.GetComponent<BoxController>().RightX == null)
        {

            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(-1, 0, 0);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        if (Box.GetComponent<BoxController>().UpY == null)
        {

            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(0, 1, 0);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        if (Box.GetComponent<BoxController>().DownY == null)
        {

            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(0, -1, 0);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        if (Box.GetComponent<BoxController>().ForwardZ == null)
        {

            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(0, 0, 1);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        if (Box.GetComponent<BoxController>().BackZ == null)
        {

            move.AppendCallback(() =>
            {
                TransparentBox.transform.position = Box.transform.position + new Vector3(0, 0, -1);
            });
            move.AppendInterval(TransParentBoxMoveTime);
        }

        move.SetLoops(-1, LoopType.Restart);
    }
}