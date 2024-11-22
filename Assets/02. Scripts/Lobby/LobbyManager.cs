using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyManager : MonoBehaviour
{
    [Header("맵 선택창")] 
    public GameObject singlePannel;
    
    [Header("멀티 플레이 팝업창")]
    public GameObject multiPannel;

    [Header("메뉴 팝업창")]
    public GameObject menuPannel;
    
    [Header("랭킹 팝업창")]
    public GameObject rankPannel;
    
    [Header("게임 씬")]
    public GameObject gameScene;
    
    [Header("로비 씬")]
    public GameObject lobbyScene;

    [Header("컨트롤 패드")] 
    public GameObject controlPad;

    [Header("싱글 플레이어")] 
    public GameObject singlePlayer;
    
    // 다른 스크립트에서 사용하기 위해 싱글톤 생성
    public static LobbyManager instance;
    
    // 매니저 스크립트 인스턴스 생성 시 실행
    void Awake()
    {
        LobbyManager.instance = this;
        // 컨트롤 패드 오브젝트 로드
        controlPad = GameObject.FindWithTag("ControlPad");
        // 초기 설정
        Initialize();
    }

    // 초기 설정 함수
    void Initialize()
    {
        // 팝업창 비활성화
        singlePannel.SetActive(false);
        multiPannel.SetActive(false);
        menuPannel.SetActive(false);
        rankPannel.SetActive(false);
        // 게임 씬 비활성화
        gameScene.SetActive(false);
        // 컨트롤 패드 비활성화
        controlPad.SetActive(false);
        // 싱글 플레이어 비활성화
        singlePlayer.SetActive(false);
    }
    
    // 맵 선택 팝업 창
    public void ShowSinglePannel()
    {
        singlePannel.SetActive(!singlePannel.activeSelf);
    }
    //멀티 플레이 팝업 창
    public void ShowMultiPannel()
    {
        multiPannel.SetActive(!multiPannel.activeSelf);
    }
    //랭킹 패널 팝업 창
    public void ShowRankPannel()
    {
        rankPannel.SetActive(!rankPannel.activeSelf);
    }
    //메뉴 패널 팝업 창
    public void ShowMenuPannel()
    {
        menuPannel.SetActive(!menuPannel.activeSelf);
    }
    // 게임 씬과 로비 씬 전환 함수
    public void SwapScene()
    {
        controlPad.SetActive(!gameScene.activeSelf);
        gameScene.SetActive(!gameScene.activeSelf);
        lobbyScene.SetActive(!gameScene.activeSelf);
    }
    
    //싱글 플레이 시작 함수
    public void StartSingleGame()
    {   
        singlePlayer.SetActive(true);
        SwapScene();
        CountManager.instance.GameStart();
    }
    
    //로비 종료 함수
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
