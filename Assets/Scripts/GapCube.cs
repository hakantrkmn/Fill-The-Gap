using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapCube : MonoBehaviour
{
    public bool IsDone;


    private void OnEnable()
    {
        EventManager.CheckCubes += CheckIfIsDone;
    }

    private void OnDisable()
    {
        EventManager.CheckCubes -= CheckIfIsDone;

    }

    public void CheckIfIsDone()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, Vector3.one * .1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<BoxController>())
            {
                IsDone = true;

            }
        }
    }
}
