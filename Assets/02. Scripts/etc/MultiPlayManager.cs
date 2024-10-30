using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiPlayManager : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1080,1920,false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnConnetedToMaster() =>
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
}
