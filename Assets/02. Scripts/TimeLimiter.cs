using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeLimiter : MonoBehaviour
{
    public static TimeLimiter instance;
    public TMP_Text timeText;
    //제한 시간
    public float timeLimit = 10f;

    void Awake()
    {
        TimeLimiter.instance = this;
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer() 
    {
        //시작 전 3초 대기
        yield return new WaitForSeconds(3f);
        
        //제한 시간 동안 플레이어 움직임 가능
        PlayerMove.instance.canMove = true;
        float remainingTime = timeLimit;
        
        while (remainingTime > 0)
        {
            //정수 형태로 표시
            timeText.text = Mathf.FloorToInt(remainingTime).ToString();
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
        
        //시간 종료 시 0으로 표시
        timeText.text = "0";
        //제한 시간 종료 후 플레이어 움직임 불가
        PlayerMove.instance.canMove = false;
    }

    public void ResetTimer()
    {
        //기존 타이머를 멈추고
        StopAllCoroutines();
        //타이머 재시작
        StartCoroutine(Timer());
    }
}
