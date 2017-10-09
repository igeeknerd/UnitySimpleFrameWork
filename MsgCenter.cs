
using System;
/** 
*Copyright(C) 201x by #John.W# 
*All rights reserved. 
*FileName:     #MsgCenter# 
*Author:       #John.W# 
*Version:      #0.10# 
*UnityVersion：#2017.1# 
*Date:         #2017.10.9# 
*Description:  A Simple Unity Muti-Game FrameWork  
*History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 模块通信中心
/// </summary>
public class MsgCenter:MonoBase{

    public static MsgCenter Instance = null;
    void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 从信息中心发送消息
    /// </summary>
    /// <param name="p_msg">消息体</param>
    public void SendMsg(MsgBase p_msg)
    {
        ProcessEvent(p_msg);
    }

    /// <summary>
    /// 外部不直接调用
    /// </summary>
    /// <param name="p_msg"></param>
    public override void ProcessEvent(MsgBase p_msg)
    {
        MsgManagerID tmpId = p_msg.GetManagerId();

        switch (tmpId)
        {
            case MsgManagerID.UIManager1:
                break;
            case MsgManagerID.UIManager2:
                break;
            case MsgManagerID.AudioManager:
                break;
            case MsgManagerID.GameManager:
                break;
            case MsgManagerID.CharacterManager:
                break;
            default:
                break;
        }
    }
}
