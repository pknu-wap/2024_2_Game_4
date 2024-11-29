using UnityEngine;

public class FishAnimation : MonoBehaviour
{
    public float scaleSpeed = 2.0f;  // 크기 변화 속도
    public float scaleAmount = 0.1f; // 크기 변화 정도
    public float tiltSpeed = 3.0f;   // 회전 속도
    public float tiltAmount = 5.0f;  // 회전 각도
    public float waveSpeed = 2.0f;   // 물결 속도
    public float waveHeight = 0.1f;  // 물결 높이

    private Vector3 originalScale;   // 초기 크기
    private Quaternion originalRotation; // 초기 회전값
    private Vector3 originalPosition; // 초기 위치값

    void Start()
    {
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
    }

    void Update()
    {
        // 크기 변화 (스프라이트 크기 애니메이션)
        float scaleX = originalScale.x + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);

        // 회전 변화 (몸 흔들림 애니메이션)
        float tilt = Mathf.Sin(Time.time * tiltSpeed) * tiltAmount;
        transform.rotation = originalRotation * Quaternion.Euler(0, 0, tilt);

        // 물결 애니메이션 (Y축 오프셋)
        //float offsetY = Mathf.Sin(Time.time * waveSpeed) * waveHeight;
        //transform.position = new Vector3(transform.position.x, originalPosition.y + offsetY, transform.position.z);
    }
}