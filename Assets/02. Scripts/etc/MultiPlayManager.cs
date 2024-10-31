using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiPlayManager : MonoBehaviourPunCallbacks
{
    
    [SerializeField]
    private GameObject gameScene;
    [SerializeField]
    private Transform gameSceneTransform;
    [SerializeField]
    private Vector3 startPosition;
    
    public static MultiPlayManager instance;
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameSceneTransform = gameScene.GetComponent<Transform>();
        startPosition = gameSceneTransform.GetChild(0).GetComponent<Transform>().localPosition;
        // 플레이어가 카메라 보다 뒤로 가도록 수정
        startPosition.z = 0;
        MultiPlayManager.instance = this;
        Screen.SetResolution(1080,1920,false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, null);        
    }

    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", startPosition, Quaternion.identity, 0);
        player.transform.SetParent(gameSceneTransform, false);
        CameraMove.instance.StartMoveCamera();
    }

}
