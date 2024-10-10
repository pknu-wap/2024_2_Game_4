using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    public float maxSpeed;
    public float jumpPower;
    public float downPower;
    public int skillNumber;
    public bool canMove = true;
    
    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayerMove());
    }

    void PlayerSkill()
    {
        if (skillNumber == 1)
        {
            StartCoroutine(SpeedUpSkill(3f));
        }
        else if (skillNumber == 2)
        {
            StartCoroutine(GodModeSkill(3f));
        }
        else if (skillNumber == 3)
        {
            //StartCoroutine(DashSkill(3f));
        }
    }
    
    IEnumerator SpeedUpSkill(float duration){
        float startTime = Time.time; // 시작 시간 기록
        maxSpeed = 6;
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }
        maxSpeed = 3;
    }

    IEnumerator GodModeSkill(float duration)
    {
        float startTime = Time.time;
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 8;
    }
    /*IEnumerator DashSkill(float duration){
        float startTime = Time.time; // 시작 시간 기록
        int dirc = transform.position.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1)*2,ForceMode2D.Impulse);
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }
    }*/

    IEnumerator PlayerMove()
    {
        while (true)
        {
            yield return null; // 즉시 실행

            // 수평 이동 구현
            if (canMove)
            {
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
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                    rigid.AddForce(Vector2.down * downPower, ForceMode2D.Impulse);
            }

            // 스프라이트 방향 전환
            if (Input.GetButton("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1; // 스프라이트의 기본이 왼쪽이면 1로 설정 오른쪽이면 -1

            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayerSkill();
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        canMove = false;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1)*2,ForceMode2D.Impulse);
        Invoke("OffDamaged", 0.5f);
      

    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        canMove = true;
    }




    void Update()
    {
        
    }
}
