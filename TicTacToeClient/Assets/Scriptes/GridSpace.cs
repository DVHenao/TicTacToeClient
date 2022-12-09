using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;


public class GridSpace : MonoBehaviour
{

    public Button button;
    public TMP_Text buttonText;
    public GameObject BlockingPanel;

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
        BlockingPanel.SetActive(true);
    }

    public void SetSpaceFromOpponent()
    {
        Debug.Log("enemy pressed");
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
        BlockingPanel.SetActive(false);
    }

}