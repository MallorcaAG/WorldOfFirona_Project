using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetworkManagerUI : NetworkBehaviour
{
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;
    [SerializeField] private Button disconnectBtn;
    [SerializeField] private TMP_InputField ipTxtField;


    private void Awake()
    {

        hostBtn.onClick.AddListener(() =>
        {
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);

            disconnectBtn.gameObject.SetActive(true);
            /*NetworkManager.Singleton.StartHost();*/


        });
        clientBtn.onClick.AddListener(() =>
        {
            hostBtn.gameObject.SetActive(false);
            clientBtn.gameObject.SetActive(false);

            disconnectBtn.gameObject.SetActive(true);


            /*NetworkManager.Singleton.StartClient();*/
        });
        disconnectBtn.onClick.AddListener(() =>
        {
            hostBtn.gameObject.SetActive(true);
            clientBtn.gameObject.SetActive(true);
            disconnectBtn.gameObject.SetActive(false);
            ipTxtField.gameObject.SetActive(true);
            NetworkManager.Singleton.Shutdown();
        });
    }

}