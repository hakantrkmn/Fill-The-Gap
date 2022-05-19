using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxController : MonoBehaviour
{
    public GameObject LeftX;
    public GameObject RightX;
    public GameObject UpY;
    public GameObject DownY;
    public GameObject ForwardZ;
    public GameObject BackZ;

    void Start()
    {
        UpdateNeigbours();
    }

    private void OnEnable()
    {
        EventManager.BoxDestroyed += BoxDestroyed;
        EventManager.UpdateNeighbours += UpdateNeigbours;
    }

    private void BoxDestroyed(GameObject destroyedBox)
    {
        if (LeftX == destroyedBox)
        {
            LeftX = null;
        }
        else if (RightX==destroyedBox)
        {
            RightX = null;
        }
        else if (UpY==destroyedBox)
        {
            UpY = null;
        }
        else if (DownY==destroyedBox)
        {
            DownY = null;
        }
        else if (ForwardZ==destroyedBox)
        {
            ForwardZ = null;
        }
        else if (BackZ==destroyedBox)
        {
            BackZ = null;
        }
    }

    private void OnDisable()
    {
        EventManager.BoxDestroyed -= BoxDestroyed;

        EventManager.UpdateNeighbours -= UpdateNeigbours;
    }

    private void OnMouseDown()
    {
        EventManager.UpdateChoosenBox(gameObject);
    }


    public void UpdateNeigbours()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(1, 0, 0), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders)
        {

                LeftX = hitCollider.gameObject;
        }

        Collider[] hitColliders2 = Physics.OverlapBox(transform.position + new Vector3(-1, 0, 0), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders2)
        {
     
                RightX = hitCollider.gameObject;
               
        }

        Collider[] hitColliders3 = Physics.OverlapBox(transform.position + new Vector3(0, -1, 0), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders3)
        {
           
                DownY = hitCollider.gameObject;
                
        }

        Collider[] hitColliders4 = Physics.OverlapBox(transform.position + new Vector3(0, 1, 0), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders4)
        {
            
                UpY = hitCollider.gameObject;
               
        }

        Collider[] hitColliders5 = Physics.OverlapBox(transform.position + new Vector3(0, 0, -1), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders5)
        {
            
                BackZ = hitCollider.gameObject;
               
        }

        Collider[] hitColliders6 = Physics.OverlapBox(transform.position + new Vector3(0, 0, 1), Vector3.one * .1f);
        foreach (var hitCollider in hitColliders6)
        {
            
                ForwardZ = hitCollider.gameObject;
               
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            EventManager.BoxHitThePuzzle();
        }
    }
}