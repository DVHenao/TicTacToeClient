using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenu : MonoBehaviour
{ 
    public GameObject gameMenuUI;
    public GameObject pauseUI;
    public void OnBackbuttonClick()
    {
        pauseUI.gameObject.SetActive(false);
        gameMenuUI.gameObject.SetActive(false);

    }

}
