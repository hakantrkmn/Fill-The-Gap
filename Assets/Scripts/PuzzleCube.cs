using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

public class PuzzleCube : MonoBehaviour
{

    public List<Vector3> points = new List<Vector3>();

    [Button]
    public void GetPoints()
    {
        points.Clear();
        var layer = LayerMask.GetMask("Puzzle");
        RaycastHit hits; 
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position,Vector3.up, out hits, Mathf.Infinity, layer))
        {
            var amount = Mathf.FloorToInt(hits.transform.position.y)  - Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                    points.Add(new Vector3(transform.position.x,transform.position.y + i + 1,transform.position.z));
            }
        }
        if (Physics.Raycast(transform.position,Vector3.down, out hits, Mathf.Infinity, layer))
        {
            var amount = -Mathf.FloorToInt(hits.transform.position.y)  + Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                points.Add(new Vector3(transform.position.x,hits.transform.position.y + i + 1,transform.position.z));
            }
        }
        if (Physics.Raycast(transform.position,Vector3.right, out hits, Mathf.Infinity, layer))
        {
            var amount = Mathf.FloorToInt(hits.transform.position.y)  - Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                points.Add(new Vector3(transform.position.x + i + 1,transform.position.y,transform.position.z));
            }
        }
        if (Physics.Raycast(transform.position,Vector3.left, out hits, Mathf.Infinity, layer))
        {
            var amount = -Mathf.FloorToInt(hits.transform.position.y)  + Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                points.Add(new Vector3(transform.position.x+ i + 1,hits.transform.position.y ,transform.position.z));
            }
        }
        if (Physics.Raycast(transform.position,Vector3.forward, out hits, Mathf.Infinity, layer))
        {
            int amount = Mathf.FloorToInt(hits.transform.position.y)  - Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                points.Add(new Vector3(transform.position.x ,transform.position.y,transform.position.z+ i + 1));
            }
        }
        if (Physics.Raycast(transform.position,Vector3.back, out hits, Mathf.Infinity, layer))
        {
            var amount = -Mathf.FloorToInt(hits.transform.position.y)  + Mathf.FloorToInt(transform.position.y) ;
            for (int i = 0; i < (int)amount-1; i++)
            {
                points.Add(new Vector3(transform.position.x,hits.transform.position.y ,transform.position.z+ i + 1));
            }
        }
       
    }
    private void OnMouseDown()
    {
        if (GameManager.instance.gameState == GameStates.PlaceBox)
        {
            EventManager.ChangeGameState(GameStates.OnPuzzle);
            EventManager.ChangeCameraToPuzzle();

        }
    }
}
