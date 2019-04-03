using UnityEngine;

namespace Astar.Data
{
    public class TerrainType
    {
        [SerializeField] private LayerMask _terrainMask;
        [SerializeField] private int _terrainPenalty;

        public LayerMask TerrainMask { get => _terrainMask; }
        public int TerrainPenalty { get => _terrainPenalty; }
    }
}

