using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

using Astar.Editor.Windows;

namespace Astar.Editor
{
    public class Navigation : EditorWindow
    {
        private enum WindowType { Baking, Terrain }
        private WindowType _previousWindow;
        private Dictionary<WindowType, Window> _windows;
        private Window _currentWindow;

        [MenuItem("Window/AI/A* Navigation")]
        public static void Init()
        {
            Navigation window = (Navigation)EditorWindow.GetWindow(typeof(Navigation), false, "A* Navigation");
            window.Show();

            window.InitializeWindows();
        }
        private void InitializeWindows()
        {
            _windows = new Dictionary<WindowType, Window>();
            _windows.Add(WindowType.Baking, new BakingWindow());
            _windows.Add(WindowType.Terrain, new TerrainWindow());

            _previousWindow = WindowType.Baking;
            LoadWindow(WindowType.Baking);
        }

        private void OnGUI()
        {
            //Draw the baking button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Baking", GUILayout.Height(30)))
                LoadWindow(WindowType.Baking);
            else if (GUILayout.Button("Terrain", GUILayout.Height(30)))
                LoadWindow(WindowType.Terrain);
            EditorGUILayout.EndHorizontal();

            _currentWindow.DrawWindow();
        }

        private void LoadWindow(WindowType windowType)
        {
            if (_previousWindow != windowType)
                _windows[_previousWindow] = _currentWindow;

            _currentWindow = _windows[windowType];
            _currentWindow.LoadWindow();

            _previousWindow = windowType;
        }
    }
}
