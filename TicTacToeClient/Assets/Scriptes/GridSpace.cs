using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class GridSpace : NetworkBehaviour
{

    public Button button;
    public TMP_Text buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    [ClientRpc]
    public void SetSpaceClientRpc()
    {

        Debug.Log("button pressed");
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
    [ServerRpc]
    public void SetSpaceServerRpc()
    {

        Debug.Log("button pressed");
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
}