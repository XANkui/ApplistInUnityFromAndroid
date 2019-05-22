using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject AppinfoItem;      // APP信息展示预制体
    public Transform AppInfoParent;     // 预制体挂置父物体
    

    List<AppInfoStruct> appInfoStructsList; // 存储APP信息的列表
    AppListTool appListTool;                // 获取APPListTool的工具类

    // Start is called before the first frame update
    void Start()
    {
        // 初始化，获取new AppListTool 工具类，并获取APP应用列表
        appListTool = new AppListTool();
        
        appInfoStructsList = new List<AppInfoStruct>();
        appInfoStructsList = appListTool.GetAppInfoList();

        // demo 测试数据
        Example();
      
    }

    /// <summary>
    /// 举例展示 APPInfo 信息
    /// </summary>
    private void Example()
    {
 
        for (int i = 0; i < 15; i++)
        {
            GameObject go1 = GameObject.Instantiate(AppinfoItem);
            go1.transform.SetParent(AppInfoParent, false);
            go1.transform.GetChild(0).GetComponent<Text>().text = appInfoStructsList[i].appName;
            go1.transform.GetChild(1).GetComponent<Text>().text = appInfoStructsList[i].packageName;
            go1.transform.GetChild(2).GetComponent<Image>().sprite = appInfoStructsList[i].appIconSprite;
            go1.transform.GetChild(3).GetComponent<RawImage>().texture = appInfoStructsList[i].appIconTexture;
        }
        
    }
}

