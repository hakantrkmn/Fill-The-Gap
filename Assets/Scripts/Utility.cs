using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility 
{
    public static void Toggle(this CanvasGroup canvasGroup)
    {
        if (canvasGroup.alpha==1)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}

