using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using UnityEngine.SceneManagement;
public class gameInfo : MonoBehaviour
{
    public TMP_Dropdown carSelection;
    public TMP_InputField userName;
    public Toggle bike;
    public TMP_InputField ipAddress;
    public TMP_Text infoText;
    RoomManagerExtension manager;

    Transform Lobby;

    private void Start()
    {
        manager = GetComponent<RoomManagerExtension>();
        infoText.gameObject.SetActive(false);
    }
    private void Update()
    {

    }

    public void host()
    {
        if (!NetworkClient.active)
        {
            // Server + Client
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                manager.StartHost();
            }
        }
        else {
            infoText.gameObject.SetActive(true);
            infoText.text = "Connecting...";
        }
    }

    public void client() {
        if (!NetworkClient.active)
        {
            if (!string.IsNullOrEmpty(ipAddress.text))
            {
                manager.networkAddress = ipAddress.text;
            }
            else
            {
                infoText.gameObject.SetActive(true);
                infoText.text = "Enter a Valid IP Address...";
            }
        }
        else
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "Connecting to " + ipAddress.text;
        }

        manager.StartClient();
    }

    public void CancelConnection() {
        manager.StopClient();
    }

    public void setSettings() {

        PlayerPrefs.DeleteAll();
        if (bike.isOn)
        {
            PlayerPrefs.SetInt("bike", 1);
        }
        else {
            PlayerPrefs.SetInt("bike", 0);
        }
        PlayerPrefs.SetInt("carSelection", carSelection.value);
        PlayerPrefs.SetString("userName", userName.text);
        PlayerPrefs.Save();
    }
}
