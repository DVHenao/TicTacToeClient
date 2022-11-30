using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum State {MainScreen,LoginScreen,RegisterScreen};

    private State stateVar;

    public GameObject mainMenuObj;
    public GameObject registerMenuObj;
    public GameObject loginMenuObj;



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


}
