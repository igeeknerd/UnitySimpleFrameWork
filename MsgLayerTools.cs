/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #MsgLayerTools# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017.1# 8
 *Date:         #2017-10-8# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消息分层常量
/// </summary>
public enum MsgManagerID
{
    GameManager = 0,
    UIManager1 = MsgLayerTools.MSG_SPAN,
    AudioManager = MsgLayerTools.MSG_SPAN * 2,
    NPCManager = MsgLayerTools.MSG_SPAN * 3,
    CharacterManager = MsgLayerTools.MSG_SPAN * 4,
    AssetsManager = MsgLayerTools.MSG_SPAN * 5,
    NetManager = MsgLayerTools.MSG_SPAN * 6,
    UIManager2 = MsgLayerTools.MSG_SPAN * 7
}
/// <summary>
/// 消息分层工具
/// </summary>
public class MsgLayerTools{
    /// <summary>
    /// 消息的分隔常量
    /// </summary>
    public const int MSG_SPAN = 3000;

}
