using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // 로비 씬 카메라
    public Camera lobbyCam;
    // 게임 씬 카메라
    public Camera gameCam;

    public enum CameraState
    {
        Lobby,
        Game
    }
    
    private static CameraManager instance = null;
    
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
    }
    
    public void SwapCamera(CameraState cam = CameraState.Lobby){
        // cam 변수에 따라 카메라 전환
        lobbyCam.enabled = cam == CameraState.Lobby ? true : false;
        gameCam.enabled  = cam == CameraState.Game ? true : false;
    }
}