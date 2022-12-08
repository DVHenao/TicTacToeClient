using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class GridSpace : MonoBehaviour
{

    public Button button;
    public TMP_Text buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }


    public void SetSpace()
    {

        Debug.Log("button pressed");
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
}