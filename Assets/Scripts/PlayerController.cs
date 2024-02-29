using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameStates gameState;
    public ClickMode clickMode;
    public GameObject box;
    public BoxController startBox;
    public List<BoxController> placedBoxes;

    private Tween tw;
    float _timer;
    public LayerMask boxLayer;

    private void OnEnable()
    {
        EventManager.ChangeClickMode += mode => clickMode = mode;
        EventManager.BoxHitThePuzzle += PuzzleHitTheBox;
        EventManager.SendPuzzle += SendPuzzle;
        EventManager.ChangeGameState += state => gameState = state;
    }


    private void OnDisable()
    {
        EventManager.ChangeClickMode -= mode => clickMode = mode;
        EventManager.ChangeGameState -= state => gameState = state;
        EventManager.BoxHitThePuzzle -= PuzzleHitTheBox;
        EventManager.SendPuzzle -= SendPuzzle;
    }

    private void PuzzleHitTheBox()
    {
        tw.Kill();
        Debug.Log("Level Failed");
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
        if (gameState == GameStates.PlaceBox)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (clickMode==ClickMode.Destroy)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, 100, boxLayer))
                    {
                        DestroyButtonClicked(hit.transform.GetComponent<BoxController>());
                        _timer = 0;
                        gameState = GameStates.PlaceBox;
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
                        gameState = GameStates.PlaceBox;
                        return;
                    }
                }
            }
        }
        else if (gameState == GameStates.BoxDestroyed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                gameState = GameStates.PlaceBox;
            }
        }
    }

    private void SendPuzzle(Transform MovePoint)
    {
        tw = transform.DOMoveZ(MovePoint.position.z, 4).OnComplete(() => { EventManager.PuzzleArrived(); });
    }
    void PlaceBox(Vector3 hitPosition, Vector3 pos)
    {
        var temp = Instantiate(box, hitPosition + pos, Quaternion.identity, transform);
        placedBoxes.Add(temp.GetComponent<BoxController>());
        EventManager.BoxPlaced(temp.GetComponent<BoxController>(),pos);
    }
}