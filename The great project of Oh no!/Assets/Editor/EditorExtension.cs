using UnityEditor;

public class EditorExtension
{
    [MenuItem("Assets/Create/Objects/Quest")]
    public static void Quest()
    {
        ScriptableObjectUtility.CreateAsset<Quest>();
    }
}
