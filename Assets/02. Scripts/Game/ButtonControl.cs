using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{
    public GameObject Player;
    PlayerMove playerMove;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerMove = Player.GetComponent<PlayerMove>();
    }
    
    public void LeftDown()
    {
        playerMove.OnLeftButtonDown();
    }
    public void LeftUp()
    {
        playerMove.OnLeftButtonUp();
    }
    public void RightDown()
    {
        playerMove.OnRightButtonDown();
    }
    public void RightUp()
    {
        playerMove.OnRightButtonUp();
    }
    public void JumpDown()
    {
        playerMove.OnJumpButtonDown();
    }
    public void JumpUp()
    {
        playerMove.OnJumpButtonUp();
    }
    public void DownDown()
    {
        playerMove.OnDownButtonDown();
    }

    public void DownUp()
    {
        playerMove.OnDownButtonUp();
    }

    public void SkillDown()
    {
        playerMove.OnSkillButtonDown();
    }

    public void SkillUp()
    {
        playerMove.OnSkillButtonUp();
    }
    
    
    
    
    void Update()
    {
        
    }
}
