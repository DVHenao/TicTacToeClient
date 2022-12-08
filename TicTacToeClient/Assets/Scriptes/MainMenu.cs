using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public enum State {MainScreen,LoginScreen,RegisterScreen, LoginComplete};

    private State stateVar;
    private string tempAccountInfo;


    public GameObject mainMenuObj;
    public GameObject registerMenuObj;
    public GameObject loginMenuObj;
    public GameObject MainParentObj;
    public GameObject GameParentObj;
    public GameObject PauseUI;
    public GameObject mainGameUI;


    public TMP_Text usernameRegistration;
    public TMP_Text passwordRegistration;

    public TMP_Text usernameLogin;
    public TMP_Text passwordLogin;

    public TMP_Text errorText;




    // Start is called before the first frame update
    void Start()
    {
        stateVar = State.MainScreen;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateVar)
        {
            case State.MainScreen:
                if (!mainMenuObj.activeInHierarchy)
                {
                    mainMenuObj.SetActive(true);
                    registerMenuObj.SetActive(false);
                    loginMenuObj.SetActive(false);
                }
                break;
            case State.LoginScreen:
                if (!loginMenuObj.activeInHierarchy)
                {
                    loginMenuObj.SetActive(true);
                    registerMenuObj.SetActive(false);
                    mainMenuObj.SetActive(false);
                }
                break;
            case State.RegisterScreen:
                if (!registerMenuObj.activeInHierarchy)
                {
                    registerMenuObj.SetActive(true);
                    mainMenuObj.SetActive(false);
                    loginMenuObj.SetActive(false);
                }
                break;
            case State.LoginComplete:
                GameParentObj.SetActive(true);
                MainParentObj.SetActive(false);
                PauseUI.SetActive(true);
                mainGameUI.SetActive(true);
                break;



        }
    }

    public void SetStateMain()
    {
        stateVar = State.MainScreen;
    }
    public void SetStateLogin()
    {
        stateVar = State.LoginScreen;
    }
    public void SetStateRegister()
    {
        stateVar = State.RegisterScreen;
    }
    public void SetStateLoginComplete()
    {
        stateVar = State.LoginComplete;
    }


    public void SaveRegistration()
    {
        tempAccountInfo = usernameRegistration.text + "," + passwordRegistration.text;
        
        if (!PlayerPrefs.HasKey("account1"))
        { 
                Debug.Log("Registering new account");
                PlayerPrefs.SetString("account1", tempAccountInfo);
                tempAccountInfo = "";
                return;
        }
        else
        {
            string[] tempuser = PlayerPrefs.GetString("account1").Split(",");
            if (tempuser[0] == usernameRegistration.text)
            {
                StartCoroutine(ShowMessage("Username already linked to an account", 3f));
                return;
            }
        }

        if (!PlayerPrefs.HasKey("account2"))
        {
            Debug.Log("Registering new account");
            PlayerPrefs.SetString("account2", tempAccountInfo);
            tempAccountInfo = "";
            return;
        }
        else
        {
            string[] tempuser = PlayerPrefs.GetString("account2").Split(",");
            if (tempuser[0] == usernameRegistration.text)
            {
                StartCoroutine(ShowMessage("Username already linked to an account", 3f));
                return;
            }
        }

        if (!PlayerPrefs.HasKey("account3"))
        {
            Debug.Log("Registering new account");
            PlayerPrefs.SetString("account3", tempAccountInfo);
            tempAccountInfo = "";
            return;
        }
        else
        {
            string[] tempuser = PlayerPrefs.GetString("account3").Split(",");
            if (tempuser[0] == usernameRegistration.text)
            {
                StartCoroutine(ShowMessage("Username already linked to an account", 3f));
                return;
            }
        }

        if (!PlayerPrefs.HasKey("account4"))
        {
            Debug.Log("Registering new account");
            PlayerPrefs.SetString("account4", tempAccountInfo);
            tempAccountInfo = "";
            return;
        }
        else
        {
            string[] tempuser = PlayerPrefs.GetString("account4").Split(",");
            if (tempuser[0] == usernameRegistration.text)
            {
                StartCoroutine(ShowMessage("Username already linked to an account", 3f));
                return;
            }
        }

        if (!PlayerPrefs.HasKey("account5"))
        {
            Debug.Log("Registering new account");
            PlayerPrefs.SetString("account5", tempAccountInfo);
            tempAccountInfo = "";
            return;
        }
        else
        {
            string[] tempuser = PlayerPrefs.GetString("account5").Split(",");
            if (tempuser[0] == usernameRegistration.text)
            {
                StartCoroutine(ShowMessage("Username already linked to an account", 3f));
                return;
            }
        }

        tempAccountInfo = "";
    }

    public void logIn()
    {
        tempAccountInfo = usernameLogin.text + "," + passwordLogin.text;

        if (tempAccountInfo == PlayerPrefs.GetString("account1"))
        {
            SetStateLoginComplete();
            Debug.Log("account 1 login successfull");
        }
        else if (tempAccountInfo == PlayerPrefs.GetString("account2"))
        {
            SetStateLoginComplete();
            Debug.Log("account 2 login successfull");
        }
        else if (tempAccountInfo == PlayerPrefs.GetString("account3"))
        {
            SetStateLoginComplete();
            Debug.Log("account 3 login successfull");
        }
        else if (tempAccountInfo == PlayerPrefs.GetString("account4"))
        {
            SetStateLoginComplete();
            Debug.Log("account 4 login successfull");
        }
        else if (tempAccountInfo == PlayerPrefs.GetString("account5"))
        {
            SetStateLoginComplete();
            Debug.Log("account 5 login successfull");
        }
        else
        {
            Debug.Log("login unsuccessful");

            string[] tempuser = PlayerPrefs.GetString("account1").Split(",");
            if (tempuser[0] == usernameLogin.text)
            {
                StartCoroutine(ShowMessage("Wrong password", 3f));
                return;
            }
            tempuser = PlayerPrefs.GetString("account2").Split(",");
            if (tempuser[0] == usernameLogin.text)
            {
                StartCoroutine(ShowMessage("Wrong password", 3f));
                return;
            }
            tempuser = PlayerPrefs.GetString("account3").Split(",");
            if (tempuser[0] == usernameLogin.text)
            {
                StartCoroutine(ShowMessage("Wrong password", 3f));
                return;
            }
            tempuser = PlayerPrefs.GetString("account4").Split(",");
            if (tempuser[0] == usernameLogin.text)
            {
                StartCoroutine(ShowMessage("Wrong password", 3f));
                return;
            }
            tempuser = PlayerPrefs.GetString("account5").Split(",");
            if (tempuser[0] == usernameLogin.text)
            {
                StartCoroutine(ShowMessage("Wrong password", 3f));
                return;
            }
            else
            {
                StartCoroutine(ShowMessage("Username and password are not registered.", 3f));
            }

        }
    }

    public void debugClearPlayerprefs()
    {
        PlayerPrefs.DeleteAll();
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);
        errorText.gameObject.SetActive(false);

    }
}
