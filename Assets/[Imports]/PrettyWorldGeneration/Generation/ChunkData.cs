using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentGeneration
{
    public class ChunkData
    {
        private HeightMap _heightMap;
        private List<Vector3> _treePositions;

        public ChunkData(HeightMap HeightMap)
        {
            _heightMap = HeightMap;

            _treePositions = new List<Vector3>();
        }

        public void AddTree(Vector3 position)
        {
            if (!_treePositions.Contains(position))
                _treePositions.Add(position);
        }
        public void AddTrees(Vector3[] positions)
        {
            for(int i = 0; i < positions.Length; i++)
            {
                if (!_treePositions.Contains(positions[i]))
                {
                    _treePositions.Add(positions[i]);
                }
            }
        }

        //Accessors
        public HeightMap HeightMap
        {
            get
            {
                return _heightMap;
            }
        }
        public Vector3[] GetAllTreePositions()
        {
                return _treePositions.ToArray();
        }
        public Vector3 GetSingleTreePosition()
        {
            int index = Random.Range(0, _treePositions.Count);
            //Debug.Log(_treePositions[index].x + ", " + _treePositions[index].y + ", " + _treePositions[index].z);
            return _treePositions[index];
        }
    }
}
