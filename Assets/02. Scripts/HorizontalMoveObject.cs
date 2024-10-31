using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMoveObject : MonoBehaviour
{
    // 이동 속도
    public float speed = 2.0f;
    // 이동 거리
    public float distance = 3.0f;

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        //시작 위치 저장
        startPosition = transform.position; 
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // 오른쪽으로 이동
            if (movingRight)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);

                // 최대 이동 거리만큼 이동했을 때 방향 전환
                if (transform.position.x >= startPosition.x + distance)
                {
                    movingRight = false;
                }
            }
            // 왼쪽으로 이동
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);

                // 최대 이동 거리만큼 이동했을 때 방향 전환
                if (transform.position.x <= startPosition.x - distance)
                {
                    movingRight = true;
                }
            }
            yield return null;
        }
    }
}
