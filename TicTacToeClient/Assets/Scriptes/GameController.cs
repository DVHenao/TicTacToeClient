using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Player
{
    public Image panel;
    public TMP_Text text;
    public Button button;
}

[System.Serializable]
public class PlayerColor
{ 
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public List<GridSpace> buttonList = new List<GridSpace>();
    public TMP_Text[] buttonListText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    public GameObject mainGameUI;

    public GameObject EventNetworkRef;


    public Button messageButton;
    public TMP_Text message;

    private string playerSide;
    private int moveCount;
    public bool stopSettingSide = true;

    void Awake()
    {
            
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
    }
    private void Update()
    {
        //if (!IsOwnedByServer)
          //  Destroy(mainGameUI);
    }
    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonListText.Length; i++)
        {
            buttonListText[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
       if (stopSettingSide) 
       {
            playerSide = startingSide;
            stopSettingSide = false;

            if (playerSide == "X")
            {
                SetPlayerColors(playerX, playerO);
                EventNetworkRef.GetComponent<NetworkedClient>().SendMessageToHost("buttonpressed,X");
                Debug.Log("test1");
            }
            else
            {
                SetPlayerColors(playerO, playerX);
                EventNetworkRef.GetComponent<NetworkedClient>().SendMessageToHost("buttonpressed,O");
                Debug.Log("test1");
            }

            StartGame();
       }
    }
    public void SetStartingSideFromOpponent(string startingSide)
    {
        if (stopSettingSide)
        {
            playerSide = startingSide;
            stopSettingSide = false;

            if (playerSide == "X")
                SetPlayerColors(playerX, playerO);
            else
                SetPlayerColors(playerO, playerX);
    
        }
            StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {


        moveCount++;

        if (buttonListText[0].text == playerSide && buttonListText[1].text == playerSide && buttonListText[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[3].text == playerSide && buttonListText[4].text == playerSide && buttonListText[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[6].text == playerSide && buttonListText[7].text == playerSide && buttonListText[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[0].text == playerSide && buttonListText[3].text == playerSide && buttonListText[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[1].text == playerSide && buttonListText[4].text == playerSide && buttonListText[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[2].text == playerSide && buttonListText[5].text == playerSide && buttonListText[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[0].text == playerSide && buttonListText[4].text == playerSide && buttonListText[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonListText[2].text == playerSide && buttonListText[4].text == playerSide && buttonListText[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }
        restartButton.SetActive(true);
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);  

        for (int i = 0; i < buttonListText.Length; i++)
        {
            buttonListText[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonListText.Length; i++)
        {
            buttonListText[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
    public void OnButtonPress(string text)
    {
        StartCoroutine(ShowMessage(text));
    }
    IEnumerator ShowMessage(string text)
    {
        message.text = text;
        message.gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        message.gameObject.SetActive(false);

    }
}