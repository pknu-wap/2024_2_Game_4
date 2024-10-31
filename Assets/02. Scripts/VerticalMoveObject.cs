using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMoveObject : MonoBehaviour
{
    //이동 속도
    public float speed = 2.0f;
    //최대 이동 거리
    public float distance = 3.0f;

    private Vector3 startPosition;
    private bool movingUp = true;

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
            if (movingUp)
            {
                //위쪽으로 이동
                transform.Translate(Vector3.up * speed * Time.deltaTime);

                //최대 거리만큼 이동하면 방향 전환
                if (transform.position.y >= startPosition.y + distance)
                {
                    movingUp = false;
                }
            }
            else
            {
                //아래쪽으로 이동
                transform.Translate(Vector3.down * speed * Time.deltaTime);

                //최대 거리만큼 이동하면 방향 전환
                if (transform.position.y <= startPosition.y - distance)
                {
                    movingUp = true;
                }
            }

            yield return null;
        }
    }
}
