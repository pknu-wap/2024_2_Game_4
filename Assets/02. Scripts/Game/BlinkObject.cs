using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlinkObject : MonoBehaviour
{
    // 타일맵 렌더러와 콜라이더 참조
    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;
    // 깜빡이는 간격 (초)
    public float blinkInterval = 1.0f;

    private void Start()
    {
        // 타일맵 렌더러와 콜라이더 가져오기
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
        // 코루틴 시작
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // 타일맵 렌더러와 콜라이더의 활성 상태를 반전
            bool isEnabled = !tilemapRenderer.enabled;
            tilemapRenderer.enabled = isEnabled;
            tilemapCollider.enabled = isEnabled;
            
            // 지정한 간격 동안 대기
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
