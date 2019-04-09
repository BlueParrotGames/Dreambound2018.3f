using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.SceneManagement;

using Astar.Data;
using Astar.Managers;
using Astar.Generators;
using Astar.Calculators;

namespace Astar
{
    public class Navigation : EditorWindow
    {
        private Vector2 _scrollPosition;

        //Baking variables
        private SceneNavigationSettings _navigationSettings;
        private float _agentRadius;
        private float _agentHeight;

        //Terrain variables
        private bool _terrainDropDown;
        private int[] _terrainLayers;
        private TerrainType[] _terrainTypes;
        private TerrainTypeAsset _terrainTypeAsset;

        //Window variables
        private enum WindowType { Baking, Terrain }
        private WindowType _currentWindow;

        [MenuItem("Window/AI/A* Navigation")]
        public static void Init()
        {
            Navigation window = (Navigation)EditorWindow.GetWindow(typeof(Navigation), false, "A* Navigation");
            window.Show();

            window.LoadVariables();
            window._currentWindow = WindowType.Baking;
        }
        private void LoadVariables()
        {
            //Load baking variables
            _navigationSettings = AssetManager.LoadAsset<SceneNavigationSettings>(GetSceneNavigationPath());
            if (_navigationSettings == null)
            {
                _navigationSettings = CreateInstance<SceneNavigationSettings>();
            }
            else
            {
                _agentRadius = _navigationSettings.AgentRadius;
                _agentHeight = _navigationSettings.AgentHeight;
            }

            //Load terrain varaiables
            _terrainTypeAsset = AssetManager.LoadAsset<TerrainTypeAsset>(GetTerrainTypesPath());
            if (_terrainTypeAsset == null)
            {
                _terrainTypeAsset = CreateInstance<TerrainTypeAsset>();
            }
            else
            {
                if (_terrainTypeAsset.WalkableRegions == null)

                    _terrainTypes = _terrainTypeAsset.WalkableRegions;
            }
            _terrainTypes = new TerrainType[0];
            _terrainLayers = new int[0];
        }

        private void OnGUI()
        {
            GUILayout.Space(20f);

            _currentWindow = (WindowType)GUILayout.Toolbar((int)_currentWindow, new string[] { "Baking", "Terrain" }, GUILayout.Height(20));
            GUILayout.Space(20f);

            //Surround the current selected window in a scroll view so we can scroll
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GetDrawWindow(_currentWindow)?.Invoke();
            GUILayout.EndScrollView();

        }

        #region Window Functions
        private System.Action GetDrawWindow(WindowType windowType)
        {
            switch (windowType)
            {
                case WindowType.Baking: return DrawBakingWindow;
                case WindowType.Terrain: return DrawTerrainWindow;
            }

            return null;
        }
        private void DrawBakingWindow()
        {
            //_unwalkableMask = EditorGUILayout.MaskField("Unwalkable Mask", _unwalkableMask, InternalEditorUtility.layers);
            _agentRadius = EditorGUILayout.FloatField("Agent Radius", _agentRadius);
            _agentHeight = EditorGUILayout.FloatField("Agent Height", _agentHeight);

            //Draw the baking button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Bake Navigation", GUILayout.Height(30)))
                BakeNavigation();
            EditorGUILayout.EndHorizontal();
        }
        private void DrawTerrainWindow()
        {
            _terrainDropDown = EditorGUILayout.Foldout(_terrainDropDown, "Walkable Regions");
            if (_terrainDropDown)
            {
                //Draw the lenght variable and adjust the array inmediately
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                int length = _terrainTypes.Length;
                length = EditorGUILayout.IntField("Length", length);
                EditTerrainTypesLenght(length);
                GUILayout.EndHorizontal();

                //Loop through the array
                for (int i = 0; i < _terrainTypes.Length; i++)
                {
                    GUILayout.Space(5);

                    //Draw the label
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label("Element " + i);
                    GUILayout.EndHorizontal();

                    //Do the layer selection
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(40);
                    _terrainLayers[i] = EditorGUILayout.Popup("Layer", _terrainLayers[i], InternalEditorUtility.layers);
                    GUILayout.EndHorizontal();
                    _terrainTypes[i].TerrainMask = LayerMask.NameToLayer(InternalEditorUtility.layers[_terrainLayers[i]]);

                    //Do the penalty
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(40);
                    _terrainTypes[i].TerrainPenalty = EditorGUILayout.IntField("Penalty", _terrainTypes[i].TerrainPenalty);
                    GUILayout.EndHorizontal();
                }
            }

            //Draw the terrain save button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save terrain settings", GUILayout.Height(30)))
                SaveTerrainTypes();
            EditorGUILayout.EndHorizontal();
        }
        #endregion

        #region Baking Functions
        private void BakeNavigation()
        {
            _navigationSettings = ScriptableObject.CreateInstance<SceneNavigationSettings>();
            UpdateNavigationSettings();

            //Save the asset
            AssetManager.SaveAsset(GetSceneNavigationPath(), _navigationSettings);
        }
        private void UpdateNavigationSettings()
        {
            MeshFilter[] meshFilters = FindObjectsOfType<MeshFilter>().Where(x => GameObjectUtility.GetStaticEditorFlags(x.gameObject) == StaticEditorFlags.NavigationStatic).ToArray();
            Vector3 worldSize = NavigationWorldSizeCalculator.GetWorldSize(meshFilters);

            //Set the values
            _navigationSettings.UpdateValues(worldSize, _agentRadius, _agentHeight);
        }
        private string GetSceneNavigationPath()
        {
            string result = "Assets/Resources/Navigation/Scene Settings/";
            result += "NAV_" + SceneManager.GetActiveScene().name + ".asset";

            return result;
        }
        #endregion

        #region Terrain Functions
        private void SaveTerrainTypes()
        {
            _terrainTypeAsset = CreateInstance<TerrainTypeAsset>();
            _terrainTypeAsset.UpdateValues(_terrainTypes);

            //Save the asset
            AssetManager.SaveAsset(GetTerrainTypesPath(), _terrainTypeAsset);
        }
        private void EditTerrainTypesLenght(int newLength)
        {
            TerrainType[] newTerrainTypes = new TerrainType[newLength];
            int[] newTerrainLayers = new int[newLength];

            System.Array.Copy(_terrainLayers, newTerrainLayers, Mathf.Min(newLength, _terrainLayers.Length));
            System.Array.Copy(_terrainTypes, newTerrainTypes, Mathf.Min(newLength, _terrainTypes.Length));

            _terrainTypes = newTerrainTypes;
            _terrainLayers = newTerrainLayers;
        }
        private string GetTerrainTypesPath() { return "Assets/Resources/Navigation/TerrainTypes.asset"; }
        #endregion
    }
}