using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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

    public TMP_Text[] buttonList;
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
    private bool stopSettingSide = true;

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
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
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

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
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

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
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