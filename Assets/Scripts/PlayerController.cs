using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public GameObject boxPrefab;
    public BoxController startBox;
    List<BoxController> placedBoxes = new List<BoxController>();

    public LayerMask boxLayer;
    private Vector3 _firstPos;
    public AnimationCurve puzzleEase;

    private void Start()
    {
        _firstPos = transform.position;
    }

    private void OnEnable()
    {
        EventManager.GetStartCubePos += GetStartCubePos;
        EventManager.BoxHitThePuzzle += PuzzleHitTheBox;
        EventManager.SendPuzzle += SendPuzzle;
    }

    private Vector3 GetStartCubePos()
    {
        return startBox.transform.position;
    }


    private void OnDisable()
    {
        EventManager.GetStartCubePos -= GetStartCubePos;
        EventManager.BoxHitThePuzzle -= PuzzleHitTheBox;
        EventManager.SendPuzzle -= SendPuzzle;
    }

    private void PuzzleHitTheBox()
    {
        DOTween.Kill("Fill");
        transform.DOMove(_firstPos, 1f).SetDelay(.3f);
    }


    private void DestroyButtonClicked(BoxController selectedBox)
    {
        if (selectedBox != startBox)
        {
            placedBoxes.Remove(selectedBox);
            EventManager.BoxDestroyed(selectedBox,
                placedBoxes.Count == 0 ? startBox : placedBoxes[Random.Range(0, placedBoxes.Count)]);
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
            if (Input.GetMouseButtonUp(0))
            {
                if (GameManager.instance.clickMode == ClickMode.Destroy)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100, boxLayer))
                    {
                        DestroyButtonClicked(hit.transform.GetComponent<BoxController>());
                        GameManager.instance.gameState = GameStates.PlaceBox;
                    }
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, boxLayer))
                    {
                        PlaceBox(hit.transform.position, hit.normal);
                            GameManager.instance.gameState = GameStates.PlaceBox;
                        
                    }
                }
            }
        }
    }

    private void SendPuzzle(Transform MovePoint)
    {
        GameManager.instance.gameState = GameStates.PuzzleOnWay;
        transform.DOMoveZ(MovePoint.position.z, 2).SetId("Fill").SetEase(puzzleEase)
            .OnComplete(() => { EventManager.PuzzleArrived(); });
    }

    void PlaceBox(Vector3 hitPosition, Vector3 pos)
    {
        var temp = Instantiate(boxPrefab, hitPosition + pos, Quaternion.identity, transform);
        placedBoxes.Add(temp.GetComponent<BoxController>());
        EventManager.BoxPlaced(temp.GetComponent<BoxController>(), pos);
    }
}