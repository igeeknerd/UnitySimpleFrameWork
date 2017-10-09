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
using UnityEngine.UI;

public class TestUIContainer : UIContainer {
    public override void ProcessEvent(MsgBase p_msg)
    {
        base.ProcessEvent(p_msg);
    }
    // Use this for initialization
    void Start () {
        this.msgIds = new ushort[] { };
        RegisterSelf(this, this.msgIds);
        UIManager.Instance.GetGameObject("Button1").GetComponent<UIBehavior>().AddNGUIBtnListener(new EventDelegate(this, "TestButtonClick"));
        UIManager.Instance.GetGameObject("Button2").GetComponent<UIBehavior>().AddNGUIBtnListener(new EventDelegate(this, "TestButtonClick1"));
    }
	
    private void TestButtonClick()
    {
        Debug.Log("这是一个笑话1");
        UISprite tmpimg = UIManager.Instance.GetGameObject("Image").GetComponent<UISprite>();
        //tmpimg.color = Color.blue;
        tmpimg.spriteName = "Flag-US";
    }

    private void TestButtonClick1()
    {
        Debug.Log("这是一个笑话2");
        //Image tmpimg = UIManager.Instance.GetGameObject("Image1").GetComponent<Image>();
        //tmpimg.color = Color.green;
        UISprite tmpimg = UIManager.Instance.GetGameObject("Image").GetComponent<UISprite>();
        //tmpimg.color = Color.blue;
        tmpimg.spriteName = "Flag-FR";
    }


    // Update is called once per frame
    void Update () {
		
	}
}
