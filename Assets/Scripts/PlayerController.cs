using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public GameObject box;
    public BoxController startBox;
    public List<BoxController> placedBoxes;

    private Tween tw;
    float _timer;
    public LayerMask boxLayer;
    private Vector3 _firstPos;
    public AnimationCurve puzzleEase;

    private void Start()
    {
        _firstPos = transform.position;
    }

    private void OnEnable()
    {
        EventManager.BoxHitThePuzzle += PuzzleHitTheBox;
        EventManager.SendPuzzle += SendPuzzle;
    }


    private void OnDisable()
    {
        EventManager.BoxHitThePuzzle -= PuzzleHitTheBox;
        EventManager.SendPuzzle -= SendPuzzle;
    }

    private void PuzzleHitTheBox()
    {
        tw.Kill();
        transform.DOMove(_firstPos, 1f).SetDelay(.3f);
    }


    private void DestroyButtonClicked(BoxController selectedBox)
    {
        if (selectedBox != startBox)
        {
            placedBoxes.Remove(selectedBox);
            EventManager.BoxDestroyed(selectedBox,placedBoxes.Count == 0 ? startBox:placedBoxes[Random.Range(0,placedBoxes.Count)]);
            selectedBox.gameObject.SetActive(false);
        }
    }

    private Ray ray;
    private RaycastHit hit;
    private BoxController lastHitBox;
    private void Update()
    {
        if (GameManager.instance.gameState == GameStates.PlaceBox)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameManager.instance.clickMode==ClickMode.Destroy)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100, boxLayer))
                    {
                        DestroyButtonClicked(hit.transform.GetComponent<BoxController>());
                        _timer = 0;
                        GameManager.instance.gameState = GameStates.PlaceBox;
                        return;
                    }
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, boxLayer))
                    {
                        //lastHitBox.ClearBoxColor();
                        PlaceBox(hit.transform.position, hit.normal);
                        _timer = 0;
                        GameManager.instance.gameState = GameStates.PlaceBox;
                        return;
                    }
                }
            }
        }
       
    }

    private void SendPuzzle(Transform MovePoint)
    {
        GameManager.instance.gameState = GameStates.PuzzleOnWay;
        tw = transform.DOMoveZ(MovePoint.position.z, 2).SetEase(puzzleEase).OnComplete(() => { EventManager.PuzzleArrived(); });
    }
    void PlaceBox(Vector3 hitPosition, Vector3 pos)
    {
        var temp = Instantiate(box, hitPosition + pos, Quaternion.identity, transform);
        placedBoxes.Add(temp.GetComponent<BoxController>());
        EventManager.BoxPlaced(temp.GetComponent<BoxController>(),pos);
    }
}