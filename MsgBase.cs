/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #MsgBase# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 
 *Date:         #2017-10-8# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息的基类
/// </summary>
public class MsgBase : MonoBehaviour {
    //消息id
    public ushort msgId;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="p_id">消息的Id,用于区别所在管理器</param>
    public MsgBase(ushort p_id)
    {
        this.msgId = p_id;
    }
    //得到消息所在管理器的ID
    public MsgManagerID GetManagerId()
    {
        int tmpId = msgId / MsgLayerTools.MSG_SPAN;
        return (MsgManagerID)(tmpId * MsgLayerTools.MSG_SPAN);
    }
    
}
