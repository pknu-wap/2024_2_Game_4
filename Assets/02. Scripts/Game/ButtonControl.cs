using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    // public GameObject Player;
    // PlayerMove playerMove;
    // void Start()
    // {
    //     Player = this.gameObject;
    //     playerMove = Player.GetComponent<PlayerMove>();
    // }
    
    public void LeftDown()
    {
        PlayerMove.instance.OnLeftButtonDown();
    }
    public void LeftUp()
    {
        PlayerMove.instance.OnLeftButtonUp();
    }
    public void RightDown()
    {
        PlayerMove.instance.OnRightButtonDown();
    }
    public void RightUp()
    {
        PlayerMove.instance.OnRightButtonUp();
    }
    public void JumpDown()
    {
        PlayerMove.instance.OnJumpButtonDown();
    }
    public void JumpUp()
    {
        PlayerMove.instance.OnJumpButtonUp();
    }
    public void DownDown()
    {
        PlayerMove.instance.OnDownButtonDown();
    }

    public void DownUp()
    {
        PlayerMove.instance.OnDownButtonUp();
    }

    public void SkillDown()
    {
        PlayerMove.instance.OnSkillButtonDown();
    }

    public void SkillUp()
    {
        PlayerMove.instance.OnSkillButtonUp();
    }
    
    
    
    
    void Update()
    {
        
    }
}
