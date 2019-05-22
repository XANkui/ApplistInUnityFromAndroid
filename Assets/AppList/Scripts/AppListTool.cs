using LitJson;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 获取APP列表工具类
/// 使用方法：
/// 1、new AppListTool();
/// 2、调用 GetAppInfoList() 函数获取返回的列表信息即可；
/// </summary>
public class AppListTool
{

    #region 对外公开使用的接口
    /// <summary>
    /// 构造函数，并做一些初始化
    /// </summary>
    public AppListTool() {
        appInfoStruct_List = new List<AppInfoStruct>();
        androidJavaObject = new AndroidJavaObject("com.example.applisttounity.AppInfoTool");
        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.LogError("该 AppListTool 功能仅支持 android 端");
        }
    }

    /// <summary>
    /// 获取应用APP数据
    /// </summary>
    /// <returns>List<AppInfoStruct> : app信息列表数据</returns>
    public List<AppInfoStruct> GetAppInfoList() {
        string str = androidJavaObject.Call<string>("GetAppInfoJsonString");
        return JsonStringParse(str);
    }

    #endregion

    #region  内部处理函数

    /// <summary>
    /// 解析 json 数据
    /// </summary>
    /// <param name="jsonString"></param>
    private List<AppInfoStruct> JsonStringParse(string jsonString)
    {
        if (jsonString == null)
        {
            Debug.Log("jsonString is null");
            return null;
        }
        JsonData jd = JsonMapper.ToObject(jsonString);
        int jd_Count = jd.Count;


        // 循环把接收到的所有 app Info 进行集合管理
        for (int i = 0; i < jd_Count; i++)
        {
            AppInfoStruct AppInfoStructItem = new AppInfoStruct();
            // 解析APP 信息
            AppInfoStructItem.appName = jd[i]["appName"].ToString();
            string packageName = jd[i]["packageName"].ToString();
            AppInfoStructItem.packageName = packageName;
            AppInfoStructItem.appIconTexture = GetAppIconByPackageName(packageName);
            AppInfoStructItem.appIconSprite = GetAppIconSpriteByPackageName(packageName);
            // 添加到列表中统一管理
            appInfoStruct_List.Add(AppInfoStructItem);

            // 性能优化，置空方便系统垃圾回收
            AppInfoStructItem = null;
        }

        return appInfoStruct_List;
    }

    /// <summary>
    /// 与安卓交互获取图片的信息
    /// </summary>
    /// <param name="packageName">对应应用的包名</param>
    /// <returns></returns>
    private Texture2D GetAppIconByPackageName(string packageName)
    {
        Texture2D tmpTexture2D = new Texture2D(AppIconTexture2D.appIconWeight, AppIconTexture2D.appIconHeight);
        byte[] tmpBytes = androidJavaObject.Call<byte[]>("getIcon", packageName);
        if (tmpBytes != null)
        {
            tmpTexture2D.LoadImage(tmpBytes);
            return tmpTexture2D;
        }

        return null;
    }

    /// <summary>
    /// 与安卓交互获取精灵图图片的信息
    /// </summary>
    /// <param name="packageName">对应应用的包名</param>
    /// <returns></returns>
    private Sprite GetAppIconSpriteByPackageName(string packageName)
    {
        Texture2D tmpTexture2D = GetAppIconByPackageName(packageName);

        if (tmpTexture2D != null) {
            Sprite sprite = Sprite.Create(tmpTexture2D, new Rect(0, 0, tmpTexture2D.width, tmpTexture2D.height), Vector2.zero);
            return sprite;
        }

        return null;
    }
    #endregion

    #region 私有变量

    AndroidJavaObject androidJavaObject;
    List<AppInfoStruct> appInfoStruct_List;

    #endregion
}

/// <summary>
/// app 信息的数据结构
/// </summary>
public class AppInfoStruct
{

    public string appName;      // 应用的名称
    public string packageName;  // 应用的包名
    public Texture2D appIconTexture;   // 应用的icon的Unity Texture2D 格式，按需要使用    
    public Sprite appIconSprite;    // 应用的icon的Unity sprite 格式 ，按需要使用
    public AppInfoStruct()
    {
        appIconTexture = new Texture2D(AppIconTexture2D.appIconWeight, AppIconTexture2D.appIconHeight);
       
    }
    
}

/// <summary>
/// APP 信息设置 icon转化为Unity中 texture2D 的长度和宽度设置类
/// </summary>
public class AppIconTexture2D {

    public static int appIconWeight = 100;            // 设置APPIcon在unity中 Texture2D 的长
    public static int appIconHeight = 100;            // 设置APPIcon在unity中 Texture2D 的宽

}
