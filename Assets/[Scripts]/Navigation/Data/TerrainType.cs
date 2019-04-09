using UnityEngine;

namespace Astar.Data
{
    [System.Serializable]
    public struct TerrainType
    {
        public LayerMask TerrainMask;
        public int TerrainPenalty;
    }
}

