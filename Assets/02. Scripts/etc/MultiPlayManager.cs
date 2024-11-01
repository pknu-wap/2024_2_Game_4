using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiPlayManager : MonoBehaviourPunCallbacks
{
    
    
    [SerializeField]
    private Vector3 startPosition;
    
    [SerializeField]
    private GameObject gameScene;
    private Transform gameSceneTransform;
    private RoomOptions roomOptions = new RoomOptions();
    
    public static MultiPlayManager instance;
    private void Awake()
    {
        gameSceneTransform = gameScene.GetComponent<Transform>();
        startPosition = gameSceneTransform.GetChild(0).GetComponent<Transform>().localPosition;
        
        Initialize();
    }

    private void Initialize()
    {
        // 방 설정 저장
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 2;
        
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
    
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장 완료");
        StartCoroutine(PlayerInitialize());
    }

    IEnumerator PlayerInitialize()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", startPosition, Quaternion.identity, 0);
        player.transform.SetParent(gameSceneTransform, false);
        LobbyManager.instance.SwapScene();
        PlayerMove.instance.Moving();
        while (PhotonNetwork.CurrentRoom.PlayerCount != roomOptions.MaxPlayers)
        {
            yield return null;
        };
        CountManager.instance.GameStart();
    }

    public void CreateRoom()
    {
        int randomInt = Random.Range(100000, 1000000);
        string roomName = randomInt.ToString();
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        PhotonNetwork.JoinRoom(roomName);
    }

    public void QuickJoinRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        
    }
}
