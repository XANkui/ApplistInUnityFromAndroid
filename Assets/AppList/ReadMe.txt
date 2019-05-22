AppListTool 简单工具类使用说明

1、new AppListTool();
2、调用 GetAppInfoList() 函数获取返回的APP列表信息；
3、操纵展示 第二 获得的信息即可；
4、APP 信息属性如下
public class AppInfoStruct
{
    public string appName;      // 应用的名称
    public string packageName;  // 应用的包名
    public Texture2D appIconTexture;   // 应用的icon的Unity Texture2D 格式，按需要使用    
    public Sprite appIconSprite;    // 应用的icon的Unity sprite 格式 ，按需要使用
}