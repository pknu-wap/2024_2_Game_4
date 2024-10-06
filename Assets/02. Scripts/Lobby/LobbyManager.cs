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
    
    // 다른 스크립트에서 사용하기 위해 싱글톤 생성
    public static LobbyManager instance = null;
    
    // 매니저 스크립트 인스턴스 생성 시 실행
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
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
    }
    
    // 맵 선택 팝업 창 표시
    public void ShowSinglePannel()
    {
        singlePannel.SetActive(!singlePannel.activeSelf);
    }

    public void ShowMultiPannel()
    {
        multiPannel.SetActive(!multiPannel.activeSelf);
    }

    public void ShowRankPannel()
    {
        rankPannel.SetActive(!rankPannel.activeSelf);
    }

    public void ShowMenuPannel()
    {
        menuPannel.SetActive(!menuPannel.activeSelf);
    }

    public void SwapScene()
    {
        gameScene.SetActive(!gameScene.activeSelf);
        lobbyScene.SetActive(!gameScene.activeSelf);
    }
    
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
