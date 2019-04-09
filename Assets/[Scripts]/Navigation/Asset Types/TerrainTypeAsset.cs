using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Data
{
    public class TerrainTypeAsset : ScriptableObject
    {
        private LayerMask _walkableMask;
        private TerrainType[] _walkableRegions;
        private Dictionary<int, int> _walkableRegionsDictionary;

        public void UpdateValues(TerrainType[] walkableRegions)
        {
            _walkableRegions = walkableRegions;

            _walkableRegionsDictionary = new Dictionary<int, int>();
            foreach (TerrainType terrainType in _walkableRegions)
            {
                _walkableMask = _walkableMask |= terrainType.TerrainMask.value;

                int key = (int)Mathf.Log(terrainType.TerrainMask.value, 2);
                _walkableRegionsDictionary.Add(key, terrainType.TerrainPenalty);
            }
        }

        public TerrainType[] WalkableRegions { get => _walkableRegions; }
        public Dictionary<int, int> WalkableRegionsDictionary { get => _walkableRegionsDictionary; }
        public LayerMask WalkableMask { get => _walkableMask; }
    }
}