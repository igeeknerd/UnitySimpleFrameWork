/** 
 *Copyright(C) 201x by #John.W# 
 *All rights reserved. 
 *FileName:     #AssetsBundleTools# 
 *Author:       #John.W# 
 *Version:      #0.10# 
 *UnityVersion：#2017-1# 
 *Date:         #2017-10-10# 
 *Description:  A Simple Unity Muti-Game FrameWork  
 *History: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
/// <summary>
/// 资源打包工具
/// </summary>
public class AssetsBundleTools {

    //构建一个测试资源包
    [MenuItem("igeeknerd.com/BuildAssetsBundleDemo")]
    public static void BuildAssetsBundleDemo()
    {
        string tpath = Application.dataPath + "/AssetsBundle";
        BuildPipeline.BuildAssetBundles(tpath, 0, EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem("igeeknerd.com/MarkAssetsBundle")]
    public static void MarkAssetsBundle()
    {
        //删除资源库中没有使用的名字
        AssetDatabase.RemoveUnusedAssetBundleNames();
        //取得文件夹数据
        string path = Application.dataPath + "Art/Scenes";
        DirectoryInfo dirinfo = new DirectoryInfo(path);
        //取得文件数据
        FileSystemInfo[] fsinfo = dirinfo.GetFileSystemInfos();
        //遍历Scenes文件夹下的文件夹
        for (int i = 0;i<fsinfo.Length; i++)
        {
            FileSystemInfo tmpinfo = fsinfo[i];
            if (tmpinfo is DirectoryInfo)
            {
                string tmppath = Path.Combine(path, tmpinfo.Name);
            }
        }
    }

    public static void SceneOverView(string p_path)
    {

    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
