using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Editor.Windows
{
    public class TerrainWindow : Window
    {
        public override void LoadWindow()
        {
            if (!Loaded)
            {
                //Do stuff
            }

            base.LoadWindow();
        }
        public override void DrawWindow()
        {
        }

        private string GetTerrainTypesPath() { return "Assets/Resources/Navigation/TerrainTypes.asset"; }
    }
}