using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerValue : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static PlayerValue instance;
    
    // 점수 관련
    public int totalPoint = 0;
    
    public TextMeshProUGUI UIPoint;
    public void SetScore()
    {
        UIPoint.text = "Score: " + totalPoint.ToString();

    }

    public void GetScore(int num)
    {
        totalPoint += num;
        SetScore();
    }

    // 체력 관련
    public float curHealth; // 현재 체력
    public float maxHealth; // 최대 체력
    public Image HpBarSlider;

    public void SetHp(float amount) // 초기 체력 세팅
    {
        maxHealth = amount;
        curHealth = maxHealth;
    }

    public void CheckHp() // HP 갱신
    {
        if (HpBarSlider != null)
            HpBarSlider.fillAmount = curHealth / maxHealth;
    }
    
    public void HpDamage(float damage) // 데미지를 받는 함수
    {
        if (maxHealth == 0 || curHealth <= 0)
            return;
        curHealth -= damage;
        CheckHp();
        if (curHealth <= 0)
        {
        }
    }

    void Awake()
    {
        SetHp(100);
        CheckHp();
        PlayerValue.instance = this;
        SetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
