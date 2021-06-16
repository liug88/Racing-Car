using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class networkPlayerSetup : NetworkBehaviour
{
    public Sprite[] cars;
    public Sprite[] bikes;

    [SyncVar(hook = "HandleDisplayNameChanged")]
    public string myName;
    [SyncVar(hook = "HandleSelectionChanged")]
    public int selection;
    [SyncVar(hook = "HandleBikeChanged")]
    public int bike;

    public void HandleDisplayNameChanged(string oldValue, string newValue) => OnSyncHook();
    public void HandleSelectionChanged(int oldValue, int newValue) => OnSyncHook();
    public void HandleBikeChanged(int oldValue, int newValue) => OnSyncHook();

    void OnSyncHook() {
        myName = PlayerPrefs.GetString("userName");
        selection = PlayerPrefs.GetInt("carSelection");
        bike = PlayerPrefs.GetInt("bike");

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players) {
            networkPlayerSetup setup = player.GetComponent<networkPlayerSetup>();
            if (setup.bike == 0) {
                player.GetComponent<SpriteRenderer>().sprite = setup.cars[setup.selection];
            }
            else{
                player.GetComponent<SpriteRenderer>().sprite = setup.bikes[setup.selection];
            }
        }
    }

    [Command]
    void CmdSetup(string name, int sel, int isB) {
        myName = name;
        selection = sel;
        bike = isB;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CmdSetup(PlayerPrefs.GetString("userName"), PlayerPrefs.GetInt("carSelection"), PlayerPrefs.GetInt("bike"));
        OnSyncHook();
    }



}
