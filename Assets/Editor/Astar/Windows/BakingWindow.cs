using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.SceneManagement;

using Astar.Editor.Data;
using Astar.Editor.Calculators;
using Astar.Editor.Managers;

namespace Astar.Editor.Windows
{
    public class BakingWindow : Window
    {
        private LayerMask _unwalkableMask;
        private SceneNavigationSettings _navigationSettings;

        public override void LoadWindow()
        {
            if (!Loaded)
            {
                //Try to load the scene's navigation settings
                _navigationSettings = AssetManager.LoadAsset<SceneNavigationSettings>(GetSceneNavigationPath());

                //Check if the loading is null if so create a new instance else update the window values
                if (_navigationSettings == null)
                    _navigationSettings = ScriptableObject.CreateInstance<SceneNavigationSettings>();
                else
                    LoadNavigationSettings();
            }

            base.LoadWindow();
        }
        public override void DrawWindow()
        {
            _unwalkableMask = EditorGUILayout.MaskField("Unwalkable Mask", _unwalkableMask, UnityEditorInternal.InternalEditorUtility.layers);

            //Draw the baking button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Bake Navigation", GUILayout.Height(30)))
                BakeNavigation();
            EditorGUILayout.EndHorizontal();
        }

        private void BakeNavigation()
        {
            _navigationSettings = ScriptableObject.CreateInstance<SceneNavigationSettings>();
            UpdateNavigationSettings();

            //Save the asset
            AssetManager.SaveAsset(GetSceneNavigationPath(), _navigationSettings);
        }

        private void LoadNavigationSettings()
        {
            _unwalkableMask = _navigationSettings.UnwalkableMask;

        }
        private void UpdateNavigationSettings()
        {
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>().Where(x => GameObjectUtility.GetStaticEditorFlags(x.gameObject) == StaticEditorFlags.NavigationStatic).ToArray();
            Vector3 worldSize = NavigationWorldSizeCalculator.GetWorldSize(meshFilters);

            _navigationSettings.UpdateValues(_unwalkableMask, worldSize);
        }

        private string GetSceneNavigationPath()
        {
            string result = "Assets/Resources/Navigation/Scene Settings/";
            result += "NAV_" + SceneManager.GetActiveScene().name + ".asset";

            return result;
        }
    }
}
