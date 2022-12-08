using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class GridSpace : MonoBehaviour
{

    public Button button;
    public TMP_Text buttonText;

    public GameObject EventNetworkRef;
    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }


    public void SetSpace()
    {
        string str = this.name.Replace("GridSpace ", "buttonpressed,");
        EventNetworkRef.GetComponent<NetworkedClient>().SendMessageToHost(str);

        Debug.Log("button pressed " + this.name);
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

    public void SetSpaceFromOpponent(string str)
    {
        Debug.Log("enemy pressed");
        buttonText.text = str;
        button.interactable = false;
        gameController.EndTurn();
    }

}