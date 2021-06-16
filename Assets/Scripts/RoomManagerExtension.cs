using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class RoomManagerExtension : NetworkRoomManager
{

    bool isReadyToStart() {

        if (numPlayers < minPlayers) {
            return false;
        }
        foreach (NetworkRoomPlayer player in roomSlots) {
            if (!player.readyToBegin) {
                return false;
            }

        }
        return true;
    }

}
