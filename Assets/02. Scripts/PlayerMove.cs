using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    public float maxSpeed;
    public float jumpPower;
    public float downPower;
    
    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayerMove());
    }

    IEnumerator PlayerMove()
    {
        while (true)
        {
            yield return null; // 즉시 실행
            
            // 수평 이동 구현
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed) // 오른쪽 최대 속도 제한
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1)) // 왼쪽 최대 속도 제한
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            
            if (Input.GetButtonUp("Horizontal")) // 키보드 입력 해제시 속도 줄이기
            {
                rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);
            }
            // 점프 구현
            if(Input.GetKeyDown(KeyCode.UpArrow))
                rigid.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            if (Input.GetKeyDown(KeyCode.DownArrow))
                rigid.AddForce(Vector2.down*downPower,ForceMode2D.Impulse);
            // 스프라이트 방향 전환
            if (Input.GetButton("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1; // 스프라이트의 기본이 왼쪽이면 1로 설정 오른쪽이면 -1
        }
    }
   
    void Update()
    {
        
    }
}
