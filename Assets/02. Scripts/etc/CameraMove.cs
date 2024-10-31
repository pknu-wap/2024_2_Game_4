using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    // 카메라 이동 속도
    public float moveSpeed = 1f;
    
    public static CameraMove instance;
    
    // <Legacy>
    // void Start()
    // {
    //     StartCoroutine(MoveCameraRight());
    // }

    void Awake()
    {
        instance = this;
    }
    
    public void StartMoveCamera()
    {
        Transform player = PlayerMove.instance.transform;
        StartCoroutine(MoveCameraRight());
    }
    
    IEnumerator MoveCameraRight()
    {
        while (true)
        {
            //매 프레임 카메라를 오른쪽으로 이동
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            
            // 한 프레임 대기
            yield return null;
        }
    }

}
