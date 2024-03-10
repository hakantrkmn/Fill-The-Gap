using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public ButtonTypes type;

    public void ButtonClicked()
    {
        switch (type)
        {
            case ButtonTypes.SendThePuzzle:
                if (!EventManager.HaveExtraCube())
                {
                    EventManager.SendButtonClicked();
                }
                else
                {
                    EventManager.LotsOfCube();
                }
                break;
            case ButtonTypes.GoBack:
                EventManager.ChangeGameState(GameStates.PlaceBox);
                EventManager.GoBackButtonClicked();
                break;
                
        }
    }
}
