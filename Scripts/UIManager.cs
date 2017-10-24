/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #UIManager# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 
 *Date:         #2017-10-9# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 处理UI部分的类,单例访问
/// </summary>
public class UIManager : ManagerBase {
    public static UIManager Instance = null;
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
    /// 在UI管理器中发送消息
    /// </summary>
    /// <param name="p_msg"></param>
    public void SendMsg(MsgBase p_msg)
    {
        //如果消息属于管理器内部，直接处理
        if (p_msg.GetManagerId() == MsgManagerID.UIManager1)
        {
            ProcessEvent(p_msg);
        }
        else//如果消息不属于当前管理器，发送到信息中心处理
        {
            MsgCenter.Instance.SendMsg(p_msg);
        }
    }

    //空间管理
    private Dictionary<string, GameObject> controlsDic = new Dictionary<string, GameObject>();
    /// <summary>
    /// 注册UI控件
    /// </summary>
    /// <param name="p_name"></param>
    /// <param name="p_obj"></param>
    public void RegisterGameObject(string p_name,GameObject p_obj)
    {
        if (!controlsDic.ContainsKey(p_name))
        {
            controlsDic.Add(p_name,p_obj);
        }
    }
    /// <summary>
    /// 移除UI控件
    /// </summary>
    /// <param name="p_name"></param>
    /// <param name="p_obj"></param>
    public void UnRegisterGameObject(string p_name,GameObject p_obj)
    {
        if (controlsDic.ContainsKey(p_name))
        {
            controlsDic.Remove(p_name);
        }
    }
    /// <summary>
    /// 取得对应UI控件
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public GameObject GetGameObject(string p_name)
    {
        if (controlsDic.ContainsKey(p_name))
        {
            return controlsDic[p_name];
        }

        return null;
    }
}
