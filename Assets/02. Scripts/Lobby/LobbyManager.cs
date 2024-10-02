using UnityEngine;
using UnityEngine.EventSystems;

public class LobbyManager : MonoBehaviour,IPointerDownHandler
{
    [Header("로비 플레이 버튼")]
    public GameObject[] playBtns = new GameObject[3];
    
    [Header("로비 메뉴 버튼")]
    public GameObject[] menuBtns = new GameObject[2];

    // 다른 스크립트에서 사용하기 위해 싱글톤 생성
    private static LobbyManager instance = null;
    
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
    }
    
    // 버튼 클릭 함수
    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
