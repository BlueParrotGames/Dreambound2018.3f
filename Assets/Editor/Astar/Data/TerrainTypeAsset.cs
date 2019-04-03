using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Astar.Data;

namespace Astar.Editor.Data
{
    public class TerrainTypeAsset : ScriptableObject
    {
        [SerializeField] private TerrainType[] _walkableRegions;

        public void UpdateValues(TerrainType[] walkableRegions)
        {
            _walkableRegions = walkableRegions;
        }

        public TerrainType[] WalkableRegions { get => _walkableRegions; }
    }
}