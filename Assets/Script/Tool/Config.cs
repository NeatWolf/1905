using UnityEngine;

public static class Config
{
    //特殊路径
    //项目的可写入路径（PC权限管理较为松散，而手机上一个应用会对应一个可写入路径）
    //Application.persistentDataPath

    //项目的Assets路径（只在PC上可以写入）
    //Application.dataPath

    //StreamingAssets路径（只在PC上可以写入）
    //Application.streamingAssetsPath

    public static string AccountJsonPath = Application.dataPath.Substring(0, Application.dataPath.Length - 7) + "/DataPath/Account.json";
}
