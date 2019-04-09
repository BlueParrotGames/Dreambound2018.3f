using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Astar.Data;
using Astar.Generators;

namespace Astar.Managers
{
    public class AstarManager : MonoBehaviour
    {
        private SceneNavigationSettings _navigationSettings;
        private TerrainTypeAsset _terrainTypes;
        private Node[,,] _grid;

        private void Awake()
        {
            _navigationSettings = Resources.Load<SceneNavigationSettings>(GetSceneNavigationPath());
            _terrainTypes = Resources.Load<TerrainTypeAsset>(GetTerrainTypesPath());

            _grid = GridGenerator.GenerateGrid(_navigationSettings, _terrainTypes);
        }

        private string GetSceneNavigationPath() { return "Navigation/Scene Settings/NAV_" + SceneManager.GetActiveScene().name; }
        private string GetTerrainTypesPath() { return "Navigation/TerrainTypes"; }

        public SceneNavigationSettings GetNavigationSettings() { return _navigationSettings; }
        public TerrainTypeAsset GetTerrainTypes() { return _terrainTypes; }
        public Node[,,] GetGrid() { return _grid; }

        public Node GetNodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x + _navigationSettings.GridWorldSize.x / 2) / _navigationSettings.GridWorldSize.x;
            float percentY = (worldPosition.y + _navigationSettings.GridWorldSize.y / 2) / _navigationSettings.GridWorldSize.y;
            float percentZ = (worldPosition.z + _navigationSettings.GridWorldSize.z / 2) / _navigationSettings.GridWorldSize.z;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);
            percentZ = Mathf.Clamp01(percentZ);

            int x = Mathf.RoundToInt((_navigationSettings.GridSize.x - 1) * percentX);
            int y = Mathf.RoundToInt((_navigationSettings.GridSize.y - 1) * percentY);
            int z = Mathf.RoundToInt((_navigationSettings.GridSize.z - 1) * percentZ);

            return _grid[x, y, z];
        }
        public List<Node> GetNeigbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        if (x == 0 && y == 0 && z == 0)
                            continue;

                        int checkX = node.GridPosition.x + x;
                        int checkY = node.GridPosition.y + y;
                        int checkZ = node.GridPosition.z + z;

                        //Check if X,Y,Z are inside the grid
                        if ((checkX >= 0 && checkX < _navigationSettings.GridSize.x) && (checkY >= 0 && checkY < _navigationSettings.GridSize.y) && (checkZ >= 0 && checkZ < _navigationSettings.GridSize.z))
                        {
                            if (_grid[checkX, checkY, checkZ] != null)
                                neighbours.Add(_grid[checkX, checkY, checkZ]);
                        }
                    }
                }
            }

            return neighbours;
        }
        public int MaxSize
        {
            get { return _navigationSettings.GridSize.x * _navigationSettings.GridSize.y * _navigationSettings.GridSize.z; }
        }
    }
}