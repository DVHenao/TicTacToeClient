using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class NetworkedClient : MonoBehaviour
{

    int connectionID;
    int maxConnections = 1000;
    int reliableChannelID;
    int unreliableChannelID;
    int hostID;
    int socketPort = 5491;
    byte error;
    bool isConnected = false;
    int ourClientID;

    public GameObject gameUI;
    public GameObject disconnectUI;
    public GameObject waitUI;
    public GameObject LoginUI;
    public GameObject BlockingPanel;
    public GameObject spectateButton;
    public bool gameStartStop = false;

    public GameObject gamecontroller;
    public GameObject MainMenu;

    public TMP_Text joinText;

    private string gameroomID;

    public TMP_Text registerUsername;
    public TMP_Text registerPassword;

    public TMP_Text loginUsername;
    public TMP_Text loginPassword;

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
            SendMessageToHost("Hello from client");

        UpdateNetworkConnection();
    }

    private void UpdateNetworkConnection()
    {
        if (isConnected)
        {
            int recHostID;
            int recConnectionID;
            int recChannelID;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostID, out recConnectionID, out recChannelID, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.ConnectEvent:
                    Debug.Log("connected.  " + recConnectionID);
                    ourClientID = recConnectionID;
                    break;
                case NetworkEventType.DataEvent:
                    string msg = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    ProcessRecievedMsg(msg, recConnectionID);
                    //Debug.Log("got msg = " + msg);
                    break;
                case NetworkEventType.DisconnectEvent:
                    isConnected = false;
                    SendMessageToHost("disconnect");
                    Debug.Log("disconnected.  " + recConnectionID);
                    break;
            }
        }
    }
    
    private void Connect()
    {
        if (!isConnected)
        {
            Debug.Log("Attempting to create connection");

            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            reliableChannelID = config.AddChannel(QosType.Reliable);
            unreliableChannelID = config.AddChannel(QosType.Unreliable);
            HostTopology topology = new HostTopology(config, maxConnections);
            hostID = NetworkTransport.AddHost(topology, 0);
            Debug.Log("Socket open.  Host ID = " + hostID);

            connectionID = NetworkTransport.Connect(hostID, "192.168.0.19", socketPort, 0, out error); // server is local on network

            if (error == 0)
            {
                isConnected = true;

                Debug.Log("Connected, id = " + connectionID);

            }
        }
    }
    
    public void Disconnect()
    {
        NetworkTransport.Disconnect(hostID, connectionID, out error);
    }
    
    public void SendMessageToHost(string msg)
    {

        // structure things in gameroom,playerid
        byte[] buffer = Encoding.Unicode.GetBytes(msg);
        NetworkTransport.Send(hostID, connectionID, reliableChannelID, buffer, msg.Length * sizeof(char), out error);
    }

    private void ProcessRecievedMsg(string msg, int id)
    {
        bool tictactoeRepeat = true;
        Debug.Log("message recieved: " + msg);
        string[] fortnite = msg.Split(',');

        switch (fortnite[0])
        {
            case "gameroomjoined":
                if(fortnite[1] == "empty") // first joined!
                {
                   LoginUI.SetActive(false);
                   gameUI.SetActive(true);
                   waitUI.SetActive(true);
                   BlockingPanel.SetActive(false);
                   spectateButton.SetActive(false);

                }
                if (fortnite[1] == "filled") //second joined! time to play!
                {
                    LoginUI.SetActive(false);
                    gameUI.SetActive(true);
                    waitUI.SetActive(false);
                    BlockingPanel.SetActive(false);
                    spectateButton.SetActive(false);

                }
                if (fortnite[1] == "spectate") //time to watch!
                {
                    LoginUI.SetActive(false);
                    gameUI.SetActive(true);
                    waitUI.SetActive(false);
                    BlockingPanel.SetActive(true);
                    spectateButton.SetActive(true);
                }
                break;

            case "disconnect":// UI cover screen informing player of DC
                waitUI.SetActive(true);
                break;

            case "buttonpressed":
                if (fortnite[1] == "otherplayerX")
                {
                    gamecontroller.GetComponent<GameController>().SetStartingSideFromOpponent("X");
                    BlockingPanel.SetActive(true);
                }
                else if (fortnite[1] == "otherplayerO")
                {
                    gamecontroller.GetComponent<GameController>().SetStartingSideFromOpponent("O");
                    BlockingPanel.SetActive(true);
                }
                else if (tictactoeRepeat)// recieving the # of the gridspace that opponent pressed
                {
                    gamecontroller.GetComponent<GameController>().buttonList[int.Parse(fortnite[1]) - 1].SetSpaceFromOpponent();
                    tictactoeRepeat = false;
                }
                break;
            case "messagesent":
                gamecontroller.GetComponent<GameController>().StartCoroutine(gamecontroller.GetComponent<GameController>().ShowMessage(fortnite[1]));
                break;

            case "loginregister":

                if (fortnite[1] == "loginsuccess")
                {
                    MainMenu.GetComponent<MainMenu>().SetStateLoginComplete();
                }
                if (fortnite[1] == "wrongpassword")
                {
                    MainMenu.GetComponent<MainMenu>().StartCoroutine(MainMenu.GetComponent<MainMenu>().ShowMessage("Wrong password", 3f));
                }
                if (fortnite[1] == "noaccount")
                {

                }
                break;

        }
    }

    public bool IsConnected()
    {
        return isConnected;
    }


    public void JoinButtonPressed()
    {
        string gameroomID = "gameroom" + "," + joinText.text; 
        SendMessageToHost(gameroomID);
    }

    public void OnSpectateButtonPressed()
    {
        LoginUI.SetActive(true);
    }

    public void registerButtonPressed()
    {
        string credentials = "register,"+ registerUsername.text +","+registerPassword.text;
        SendMessageToHost(credentials);
    }

    public void loginButtonPressed()
    {
        string credentials = "login," + loginUsername.text.Remove(loginUsername.text.Length-1) + "," + loginPassword.text.Remove(loginPassword.text.Length - 1);
        SendMessageToHost(credentials);
    }

}
