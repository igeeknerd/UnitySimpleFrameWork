/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #MonoBase# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017-1# 
 *Date:         #2017-10-8# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 扩展MonoBeh，备用今后的扩展
/// </summary>
public abstract class MonoBase : MonoBehaviour {

    public abstract void ProcessEvent(MsgBase p_msg);
}
