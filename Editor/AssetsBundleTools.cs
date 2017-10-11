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
/// AssetsBundle资源打包工具
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
        string path = Application.dataPath + "/Scenes";
        //Debug.Log("文件夹为 " + path);
        DirectoryInfo dirinfo = new DirectoryInfo(path);
        //取得文件数据
        FileSystemInfo[] fsinfo = dirinfo.GetFileSystemInfos();
        //遍历Scenes文件夹下的文件夹
        for (int i = 0;i<fsinfo.Length; i++)
        {
            FileSystemInfo tmpinfo = fsinfo[i];
            if (tmpinfo is DirectoryInfo)
            {
                string tmppath = path + "/" + tmpinfo.Name;
                //Debug.Log("path is " + path + " name is " + tmpinfo.Name);
                SceneOverView(tmppath,tmpinfo.Name);
            }
            else//是一个场景文件
            {

            }
        }
    }
    /// <summary>
    /// 便利一个场景的文件夹
    /// </summary>
    /// <param name="p_path">Art/Scenes下场景文件夹的位置</param>
    public static void SceneOverView(string p_path,string p_scene_name)
    {
        //路径
        string txtName = "Log.txt";
        string tmppath = p_path +"/" +txtName;
        //打开文件流
        FileStream fs = new FileStream(tmppath, FileMode.OpenOrCreate);
        StreamWriter sw = new StreamWriter(fs);
        Dictionary<string, string> readDic = new Dictionary<string, string>();

        ModuleOverView(p_path,p_scene_name);

        //Debug.Log("当前的目录是 " + tmppath);
        sw.Close();
        fs.Close();

    }
    /// <summary>
    /// 处理场景文件夹下的模块文件夹
    /// </summary>
    /// <param name="p_path"></param>
    public static void ModuleOverView(string p_path, string p_scene_name)
    {
        DirectoryInfo info = new DirectoryInfo(p_path);
        FileSystemInfo[] fsinfo = info.GetFileSystemInfos();
        
        for (int i= 0;i < fsinfo.Length;i++)
        {
            
            FileSystemInfo tmpfs = fsinfo[i];
            //如果文件是模块的文件夹,处理模块文件夹中的文件
            if (tmpfs is DirectoryInfo)
            {
                Debug.Log("模块文件夹的名字是 " + fsinfo[i].Name);
                FileOverView(tmpfs.FullName,p_scene_name, fsinfo[i].Name);
            }
            else//是场景或者meta文件
            {
                if(fsinfo[i].Name.IndexOf(".meta") == -1)
                {
                    //排除。meta文件
                    ChangeSCABName(tmpfs.FullName, p_scene_name, fsinfo[i].Name);
                }
                
            }
        }
    }
    /// <summary>
    /// 遍历模块中的除了.meta以外的所有文件
    /// </summary>
    /// <param name="p_path"></param>
    public static void FileOverView(string p_path, string p_scene_name,string p_module_name)
    {
        DirectoryInfo info = new DirectoryInfo(p_path);
        FileSystemInfo[] fsinfo = info.GetFileSystemInfos();

        for (int i = 0; i < fsinfo.Length; i++)
        {
            //Debug.Log("文件的名字是 " + fsinfo[i].Name);
            FileSystemInfo tmpfs = fsinfo[i];
            //如果文件是模块的文件夹,处理模块文件夹中的文件
            if (tmpfs.Name.IndexOf(".meta") == -1)
            {
                Debug.Log("文件的名字是 " + fsinfo[i].Name);
                ChangeABName(fsinfo[i].FullName,p_scene_name,p_module_name);
            }
        }
    }
    /// <summary>
    /// 改变文件的assetsbundle名字的后缀名字,注意命名中不要包含unity
    /// </summary>
    /// <param name="p_fullpath"></param>
    /// <param name="p_scene_name"></param>
    /// <param name="p_module_name"></param>
    public static void ChangeABName(string p_fullpath,string p_scene_name,string p_module_name)
    {
        int tmpindex = p_fullpath.IndexOf("Assets");
        string tmpstr = p_fullpath.Substring(tmpindex, p_fullpath.Length - tmpindex);
        Debug.Log("截取的字符串为 " + tmpstr);
        AssetImporter tmpimp = AssetImporter.GetAtPath(tmpstr);
        tmpimp.assetBundleName = p_scene_name + "/" + p_module_name;

        if(tmpstr.IndexOf(".unity") > 0){
            tmpimp.assetBundleVariant = "u3dsc";
        }
        else
        {
            tmpimp.assetBundleVariant = "u3dres";
        }
        
    }
    /// <summary>
    /// 场景文件单独处理
    /// </summary>
    /// <param name="p_fullpath"></param>
    /// <param name="p_scene_name"></param>
    /// <param name="p_module_name"></param>
    public static void ChangeSCABName(string p_fullpath, string p_scene_name, string p_module_name)
    {
        int tmpindex = p_fullpath.IndexOf("Assets");
        string tmpstr = p_fullpath.Substring(tmpindex, p_fullpath.Length - tmpindex);
        Debug.Log("截取的字符串为 " + tmpstr);
        AssetImporter tmpimp = AssetImporter.GetAtPath(tmpstr);
        //场景文件只用场景文件夹标记
        tmpimp.assetBundleName = p_scene_name;

        if (tmpstr.IndexOf(".unity") > 0)
        {
            tmpimp.assetBundleVariant = "u3dsc";
        }
        else
        {
            tmpimp.assetBundleVariant = "u3dres";
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
