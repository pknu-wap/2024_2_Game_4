using UnityEngine;
using System.Collections;

public class CameraBoundaries : MonoBehaviour
{
    
    //플레이어 오브젝트
    public Transform player;
    public Camera cam;
    
    //카메라의 경계를 제한할 변수들
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    //플레이어의 반지름 (오프셋 계산을 위한 변수)
    private float playerHalfWidth;
    private float playerHalfHeight;

    void Start()
    {
        player = this.GetComponent<Transform>();
        //카메라의 반높이, 반너비 계산
        cam = Camera.main;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * cam.aspect;
        
         if (player != null)
         {
             SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
             playerHalfWidth = playerRenderer.bounds.extents.x;
             playerHalfHeight = playerRenderer.bounds.extents.y;
         }
         
        StartCoroutine(CheckPlayerBounds());
    }
    
    IEnumerator CheckPlayerBounds()
    {
        player.position = new Vector3(cameraHalfWidth+10, 0, 0);
        while (true)
        {
            //카메라의 현재 위치를 지속적으로 가져옴
            Vector3 cameraPos = cam.transform.position;

            //카메라 경계 계산 (플레이어 크기 고려)
            float minX = cameraPos.x - cameraHalfWidth + playerHalfWidth;
            float maxX = cameraPos.x + cameraHalfWidth - playerHalfWidth;
            //플레이어가 있을 때만 경계 체크
            if (player != null)
            {
                //플레이어의 현재 위치
                Vector3 playerPos = player.position;

                //플레이어의 X 좌표만 제한
                playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);

                //플레이어 위치 적용 (Y축은 그대로 둠)
                player.position = new Vector3(playerPos.x, player.position.y, player.position.z);
            }

            //한 프레임 대기
            yield return null;
        }
    }
}