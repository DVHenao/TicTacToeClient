using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    public GameObject PreGameMenu;

    private void Awake()
    {
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            PreGameMenu.SetActive(false);
        });
        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            PreGameMenu.SetActive(false);
        });
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
