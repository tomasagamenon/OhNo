using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if(path == "")
        {
            path = "Assets";
        }
        else if(Path.GetExtension(path) != "")
        {
            var p = AssetDatabase.GetAssetPath(Selection.activeObject);
            var rep = Path.GetFileName(p);
            path = path.Replace(rep, "");
        }

        var ap = path + "/New" + typeof(T).ToString() + ".asset";
        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(ap);
        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
