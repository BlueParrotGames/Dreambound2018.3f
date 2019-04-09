using System;
using UnityEditor;

namespace Astar.Managers
{
    public class AssetManager
    {
        public static T LoadAsset<T>(string path)
        {
            return (T)(object)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }
        public static void SaveAsset(string path, UnityEngine.Object scriptableObject)
        {
            string[] segments = path.Split('/');
            string folderPath = "";
            string parentFolderPath = "";
            for(int i = 0; i < segments.Length - 1; i++)
            {
                //Construct all the needed strings
                folderPath += segments[i];
                if (i < segments.Length - 2)
                {
                    folderPath += '/';

                    parentFolderPath += segments[i];
                    if (i < segments.Length - 3)
                        parentFolderPath += '/';
                }
            }

            if (!AssetDatabase.IsValidFolder(folderPath))
                AssetDatabase.CreateFolder(parentFolderPath, segments[segments.Length - 2]);

            //Delete the old version of the asset
            try { AssetDatabase.DeleteAsset(path); } catch { }

            //Save the asset
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
        }
    }
}
