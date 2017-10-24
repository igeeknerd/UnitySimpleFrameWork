
using System;
/** 
*Copyright(C) 201x by #John.W# 
*All rights reserved. 
*FileName:     #SCRIPTFULLNAME# 
*Author:       #AUTHOR# 
*Version:      #VERSION# 
*UnityVersion：#UNITYVERSION# 
*Date:         #DATE# 
*Description:  A Simple Unity Muti-Game FrameWork  
*History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ui容器
/// </summary>
public class UIContainer : MonoBase {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy()
    {
        if (msgIds != null)
        {
            UnRegisterSelf(this,msgIds);
            this.msgIds = null;
        }
    }
    public ushort[] msgIds;
    public void RegisterSelf(MonoBase p_base,params ushort[] p_msg)
    {
        UIManager.Instance.RegisterMsgs(p_base, p_msg);
    }

    public void UnRegisterSelf(MonoBase p_base, params ushort[] p_msg)
    {
        UIManager.Instance.UnRegisterMsgs( p_msg,p_base);
    }

    public void SendMsg(MsgBase p_msg)
    {
        UIManager.Instance.SendMsg(p_msg);
    }
    public override void ProcessEvent(MsgBase p_msg)
    {
        throw new NotImplementedException();
    }
}
