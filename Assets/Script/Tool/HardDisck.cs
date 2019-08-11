using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class HardDisck 
{
    public static bool FileExists(string path) {

        return File.Exists(path);
    }

    public static string GetWriteablePath(string dirPath)
    {
        #if UNITY_EDITOR
        return Application.dataPath.Remove(Application.dataPath.Length - 7) + "/DataPath" + dirPath;
        #else
            return Application.persistentDataPath + dirPath;
        #endif
    }
}
