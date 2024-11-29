using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMove : MonoBehaviour
{
    
    // 카메라 이동 속도
    public float moveSpeed = 1f;
    public bool canMove = true;
    public static CameraMove instance;
    public Vector3 defaultPosition;
    
    // <Legacy>
    // void Start()
    // {
    //     StartCoroutine(MoveCameraRight());
    // }

    void Awake()
    {
        CameraMove.instance = this;
        defaultPosition = transform.position;
    }

    public void stopMove()
    {
        canMove = false;
    }
    
    public void StartMoveCamera()
    {
        transform.position = defaultPosition;
        Transform player = PlayerMove.instance.transform;
        StartCoroutine(MoveCameraRight());
    }
    
    IEnumerator MoveCameraRight()
    {
        while (canMove)
        {
            //매 프레임 카메라를 오른쪽으로 이동
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            
            // 한 프레임 대기
            yield return null;
        }
    }

}
