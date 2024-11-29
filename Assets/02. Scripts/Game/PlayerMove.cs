using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviourPunCallbacks
{
    public static PlayerMove instance;
    
    Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;
    public float maxSpeed;
    public float jumpPower;
    public float downPower;
    public int skillNumber;
    [SerializeField]
    public bool canMove = true;
    
    private PhotonView pView;
    
    
    
    // 버튼 조작 관련
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputJump = false;
    public bool inputDown = false;
    public bool inputSkill = false;

    public void OnLeftButtonDown() => inputLeft = true;
    public void OnLeftButtonUp() => inputLeft = false;
    public void OnRightButtonDown() => inputRight = true;
    public void OnRightButtonUp() => inputRight = false;
    public void OnJumpButtonDown() => inputJump = true;
    public void OnJumpButtonUp() => inputJump = false;
    public void OnDownButtonDown() => inputDown = true;
    public void OnDownButtonUp() => inputDown = false;
    public void OnSkillButtonDown() => inputSkill = true;
    public void OnSkillButtonUp() => inputSkill = false;

    void Awake()
    {
        PlayerMove.instance = this;
        rigid = GetComponent<Rigidbody2D>();
        pView = GetComponent<PhotonView>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Moving()
    {
        StartCoroutine(PlayerMoving());
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
            StartCoroutine(DashSkill(1f));
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
    IEnumerator DashSkill(float duration){
        float startTime = Time.time; // 시작 시간 기록
        int dirc = spriteRenderer.flipX ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,0.5f)*10,ForceMode2D.Impulse);
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        while (Time.time < startTime + duration)
        {
            yield return null; // 한 프레임 대기
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 8;
    }

    IEnumerator PlayerMoving()
    {
        while (true)
        {
            yield return null; // 즉시 실행
            //Debug.Log("플레이어 입력 대기중");
            // 수평 이동 구현
            if (canMove)
            {
                float h = 0;
                h = Input.GetAxisRaw("Horizontal");
                if (inputLeft)
                    h = -1;
                else if (inputRight)
                    h = 1;
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

                if (rigid.velocity.x > maxSpeed) // 오른쪽 최대 속도 제한
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                else if (rigid.velocity.x < maxSpeed * (-1)) // 왼쪽 최대 속도 제한
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

                /*if (Input.GetButtonUp("Horizontal")) // 키보드 입력 해제시 속도 줄이기
                {
                    rigid.velocity = new Vector2(0.5f * rigid.velocity.normalized.x, rigid.velocity.y);
                }*/

                // 점프 구현
                if (Input.GetKeyDown(KeyCode.UpArrow) || inputJump)
                {
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    OnJumpButtonUp();
                }

                if (Input.GetKeyDown(KeyCode.DownArrow) || inputDown)
                {
                    rigid.AddForce(Vector2.down * downPower, ForceMode2D.Impulse);
                    OnDownButtonUp();
                }
            }

            // 스프라이트 방향 전환
            if (Input.GetButton("Horizontal"))
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1; // 스프라이트의 기본이 왼쪽이면 1로 설정 오른쪽이면 -1
            if (inputRight)
                spriteRenderer.flipX = inputLeft;
            else if (inputLeft)
                spriteRenderer.flipX = inputLeft;
                
            // 스킬 사용
            if (Input.GetKeyDown(KeyCode.X) || inputSkill)
            {
                PlayerSkill();
                OnSkillButtonUp();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            //점수
            Debug.Log(collision.gameObject.name);
            bool isBronze = collision.gameObject.name.Contains("coinBronze");
            bool isSilver = collision.gameObject.name.Contains("coinSilver");
            bool isGold = collision.gameObject.name.Contains("coinGold");
            collision.gameObject.SetActive(false);
            if (isBronze)
                PlayerValue.instance.GetScore(50);
            else if (isSilver)
                PlayerValue.instance.GetScore(150);
            else if (isGold)
                PlayerValue.instance.GetScore(300);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 장애물과 충돌
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 hitPosition = Vector3.zero;
        
            // Tilemap 콜라이더의 정확한 충돌 지점을 가져옴
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition = hit.point; // 충돌한 지점의 월드 좌표
                break; // 첫 번째 충돌 지점만 사용
            }
            
            OnDamaged(hitPosition,0);
        }
        

        if (collision.gameObject.tag == "EndEnemy")
        {
            OnDamaged(collision.transform.position,1);
        }
    }

    void OnDamaged(Vector2 targetPos,int type)
    {
        Debug.Log($"Player Position: {transform.position}, Collision Position: {targetPos}");
        canMove = false;
        rigid.velocity = Vector3.zero;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 색상 변경, 피격 표시
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        if (type == 0)
        {
            PlayerValue.instance.HpDamage(5);
            rigid.AddForce(new Vector2(dirc, 1) * 2, ForceMode2D.Impulse);
            Invoke("OffDamaged", 0.7f);
        }
        else if (type == 1)
        {
            PlayerValue.instance.HpDamage(100);
        }

    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        gameObject.layer = 8;
        canMove = true;
    }
    

    void CanMoveStart()
    {
        canMove = true;
    }

    public GameObject GetPlayerObject()
    {
        return this.gameObject;
    }

    void Update()
    {
        
    }
}
