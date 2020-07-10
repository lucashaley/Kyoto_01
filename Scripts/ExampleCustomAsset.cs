using UnityEngine;

// Custom assets have to derive from ScriptableObject
public class ExampleCustomAsset : ScriptableObject
{
    // like serialized classes,
    // if it's just data storage make the variables public
    public float floatParam;
    public ExampleCustomAsset objectParam;

#if UNITY_EDITOR
    // Now we need a builder
    [UnityEditor.MenuItem("Assets/Create/ExampleCustomAsset")]
    public static void BuildAsset()
    {
        CustomAssetUtil.CreateTheAsset<ExampleCustomAsset>();
    }

#endif
}

#if UNITY_EDITOR
public static class CustomAssetUtil
{
    public static void CreateTheAsset<T> () where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T> ();

        string path = UnityEditor.AssetDatabase.GetAssetPath (UnityEditor.Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (System.IO.Path.GetExtension (path) != "")
        {
            path = path.Replace (System.IO.Path.GetFileName (UnityEditor.AssetDatabase.GetAssetPath (UnityEditor.Selection.activeObject)), "");
        }

        string assetPathAndName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");

        UnityEditor.AssetDatabase.CreateAsset (asset, assetPathAndName);

        UnityEditor.AssetDatabase.SaveAssets ();
    	UnityEditor.EditorUtility.FocusProjectWindow ();
		UnityEditor.Selection.activeObject = asset;
	}
}
#endif
