using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportAssetBundles : MonoBehaviour
{

    #region Config

    /// <summary>
    /// AssetBundle存储路径
    /// </summary>
    private static string _SaveDirectory = "/DataPath" + MyConfig.AssetBundlesSavePathName;

    /// <summary>
    /// AssetBundle选项
    /// </summary>
    private static BuildAssetBundleOptions _BundleOption = BuildAssetBundleOptions.ForceRebuildAssetBundle;

    #endregion

    #region Export

    [MenuItem("AssetBundles/Export(Windows)")]
    static void WindowsExport()
    {
        _Build(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("AssetBundles/Export(Mac)")]
    static void MacExport()
    {
        _Build(BuildTarget.StandaloneOSX);
    }

    [MenuItem("AssetBundles/Export(iOS)")]
    static void iOSExport()
    {
        _Build(BuildTarget.iOS);
    }

    [MenuItem("AssetBundles/Export(Android)")]
    static void AndroidExport()
    {
        _Build(BuildTarget.Android);
    }

    #endregion

    #region Helper

    private static void _Build(BuildTarget platform)
    {
        if (EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        string realPath = Application.dataPath.Remove(Application.dataPath.Length - 7) + _SaveDirectory;

        if (!Directory.Exists(realPath))
        {
            Directory.CreateDirectory(realPath);
        }

        BuildPipeline.BuildAssetBundles(realPath, _BundleOption, platform);

        if (MyConfig.CopyToStreamingAssets)
        {
            CopyAB(realPath, MyConfig.ABCopyPath);
            WriteInConfig(MyConfig.ABCopyPath);
        }

        Debug.Log("AssetBundle生成完成！路径：" + realPath);
    }

    #endregion

    /// <summary>
    /// 将streamingAssetsPath下所有文件和第一层文件下的子文件的文件名用；分割写入Config.txt文件
    /// </summary>
    /// <param name="readPath"></param>
    private static void WriteInConfig(string readPath)
    {
        string config = "";
        WriteDirInConfig("", readPath, ref config);

        File.WriteAllText(MyConfig.ABConfigName, config);
        Debug.Log("配置文件生成完成：" + MyConfig.ABConfigName);
    }

    /// <summary>
    /// 将所有文件的相对路径写入字符串中
    /// </summary>
    /// <param name="rootPath"></param>当前文件夹的相对根路径
    /// <param name="currentReadPath"></param>当前遍历到的文件夹
    /// <param name="config"></param>字符串
    /// <returns></returns>
    private static void WriteDirInConfig(string rootPath, string currentReadPath, ref string config)
    {
        string[] paths = Directory.GetFiles(currentReadPath);// 获取每个文件的完整路径
        string[] dirPaths = Directory.GetDirectories(currentReadPath);// 每个文件夹目录

        for (int i = 0; i < paths.Length; i++)
        {
            config += rootPath + Path.GetFileName(paths[i]) + ";";
        }

        for (int i = 0; i < dirPaths.Length; i++)
        {
            WriteDirInConfig(Path.GetFileName(dirPaths[i]) + "/", dirPaths[i], ref config);
        }
    }

    //复制目录下所有文件 
    private static void CopyAB(string readPath, string writePath)
    {
        if (!Directory.Exists(writePath))
        {
            Directory.CreateDirectory(writePath);
        }
        string[] files = Directory.GetFiles(readPath);
        for (int i = 0; i < files.Length; i++)
        {
            File.Copy(files[i], writePath + "/" + Path.GetFileName(files[i]), true);
        }
    }

}