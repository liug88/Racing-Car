
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class playerInfo : NetworkRoomPlayer
{
    public Sprite[] cars;
    public Sprite[] bikes;

    [SyncVar(hook = nameof(HandleDisplayNameChanged))]
    public string playerName;
    [SyncVar(hook = nameof(HandleReadyChanged))]
    public bool ready;
    [SyncVar(hook = nameof(HandleSelectionChange))]
    public int selection;
    [SyncVar(hook = nameof(HandleBikeChoice))]
    public int bikeSelection;


    public void HandleDisplayNameChanged(string oldValue, string newValue) => UpdateDisplay();
    public void HandleReadyChanged(bool oldValue, bool newValue) => UpdateDisplay();
    public void HandleSelectionChange(int oldValue, int newValue) => UpdateDisplay();
    public void HandleBikeChoice(int oldValue, int newValue) => UpdateDisplay();
    public GameObject readyButton;
    public GameObject backButton;

    private void UpdateDisplay()
    {

        RoomManagerExtension Room = GameObject.Find("Game Manager").GetComponent<RoomManagerExtension>();
        if (!hasAuthority)
        {

            foreach (playerInfo player in Room.roomSlots)
            {
                if (player.hasAuthority)
                {
                    player.UpdateDisplay();
                    break;
                }
            }

            return;
        }



        Transform lobby = GameObject.Find("Lobby").transform;

        for (int i = 0; i < lobby.childCount; i++)
        {
            lobby.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = "Waiting For Player ...";
            lobby.GetChild(i).GetChild(2).gameObject.SetActive(false);

        }
        for (int i = 0; i < Room.roomSlots.Count; i++)
        {
            string userName = Room.roomSlots[i].GetComponent<playerInfo>().playerName;

            lobby.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = userName;
            lobby.GetChild(i).GetChild(2).gameObject.SetActive(true);
            if (Room.roomSlots[i].GetComponent<playerInfo>().ready)
            {
                lobby.GetChild(i).GetChild(2).GetComponent<TMP_Text>().color = Color.green;
                lobby.GetChild(i).GetChild(2).GetComponent<TMP_Text>().text = "Ready";
            }
            else
            {
                lobby.GetChild(i).GetChild(2).GetComponent<TMP_Text>().color = Color.red;
                lobby.GetChild(i).GetChild(2).GetComponent<TMP_Text>().text = "Not Ready";
            }

            if (Room.roomSlots[i].GetComponent<playerInfo>().bikeSelection == 1) {
                lobby.GetChild(i).GetChild(3).GetComponent<Image>().sprite = bikes[Room.roomSlots[i].GetComponent<playerInfo>().selection];
            }
            else {
                lobby.GetChild(i).GetChild(3).GetComponent<Image>().sprite = cars[Room.roomSlots[i].GetComponent<playerInfo>().selection];
            }
            

        }


    }



    public override void OnStartAuthority()
    {
        CmdCarSelection(PlayerPrefs.GetInt("carSelection"), PlayerPrefs.GetInt("bike"));

        CmdSetDisplayName(PlayerPrefs.GetString("userName"));
        
        

    }

    [Command]
    private void CmdCarSelection(int s, int bike)
    {
        bikeSelection = bike;
        selection = s;
    }

    [Command]
    private void CmdSetDisplayName(string displayName)
    {

        playerName = displayName;
    }

    [Command]
    private void CmdReadyUp()
    {

        ready = readyToBegin;
    }

    public override void OnClientEnterRoom()
    {
        base.OnClientEnterRoom();
        UpdateDisplay();

        readyButton = GameObject.Find("Ready");
        readyButton.GetComponent<Button>().onClick.AddListener(delegate { readyUp(); });


        backButton = GameObject.Find("Back");
        backButton.GetComponent<Button>().onClick.AddListener(delegate { disconnectMe(); });
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        UpdateDisplay();
    }


    public void disconnectMe()
    {

        GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
    }

    public override void ReadyStateChanged(bool _, bool newReadyState)
    {
        base.ReadyStateChanged(_, newReadyState);
        UpdateDisplay();
    }
    public void readyUp()
    {
        if (NetworkClient.active && isLocalPlayer)
        {


            readyToBegin = !readyToBegin;
            
            CmdChangeReadyState(readyToBegin);
            CmdReadyUp();
            if (readyToBegin)
            {
                readyButton.GetComponentInChildren<TMP_Text>().text = "Cancel";

            }
            else
            {
                readyButton.GetComponentInChildren<TMP_Text>().text = "Ready!";

            }


        }

    }


}
