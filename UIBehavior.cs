/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #UIBehavior# 
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
using UnityEngine.UI;
using UnityEngine.Events;
/// <summary>
/// 处理UI控件的插件类
/// </summary>
public class UIBehavior : MonoBehaviour {

    void Awake()
    {
        //注册到缓存中
        UIManager.Instance.RegisterGameObject(name,this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// 添加按钮事件
    /// </summary>
    /// <param name="p_action"></param>
    public void AddBtnListener(UnityAction p_action)
    {
        if(p_action != null)
        {
            Button btn = this.transform.GetComponent<Button>();
            btn.onClick.AddListener(p_action);
        }
    }
    /// <summary>
    /// 删除按钮事件
    /// </summary>
    /// <param name="p_action"></param>
    public void RMBtnListener(UnityAction p_action)
    {
        if (p_action != null)
        {
            Button btn = this.transform.GetComponent<Button>();
            btn.onClick.RemoveListener(p_action);
        }
    }
}
