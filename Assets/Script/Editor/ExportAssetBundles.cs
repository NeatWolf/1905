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
    private static string _SaveDirectory = "/DataPath" + Setting.AssetBundlesSavePathName;

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

        Debug.Log("AssetBundle生成完成！路径：" + realPath);
    }

    #endregion

}