using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentGeneration
{
    public class EnvironmentGenerator
    {
        private HeightMap _worldHeightMap;
        private MeshSettings _meshSettings;
        private TextureData _textureData;

        private Dictionary<Vector2, ChunkData> _terrainChunks;

        //private TreeGenerator _treeGenerator;

        public EnvironmentGenerator(HeightMap WorldHeightMap, MeshSettings MeshSettings, TextureData TextureData)
        {
            _worldHeightMap = WorldHeightMap;
            _meshSettings = MeshSettings;
            _textureData = TextureData;

            //_treeGenerator = new TreeGenerator(WorldHeightMap, _meshSettings, TextureData);

            _terrainChunks = new Dictionary<Vector2, ChunkData>();
        }

        public void RegisterChunk(Vector2 Coords, HeightMap HeightMap)
        {
            ChunkData chunkData = new ChunkData(HeightMap);

            if (!_terrainChunks.ContainsKey(Coords))
            {
                //chunkData.AddTrees(_treeGenerator.GetTreePositions(HeightMap));
                _terrainChunks.Add(Coords, chunkData);
            }
        }

        public Vector3[] GetChunkTrees(Vector2 Coords)
        {
            if (_terrainChunks.ContainsKey(Coords))
            {
                return _terrainChunks[Coords].GetAllTreePositions();
            }
            else
            {
                return null;
            }
        }
        public Vector3 GetChunkTree(Vector2 Coords)
        {
            if (_terrainChunks.ContainsKey(Coords))
            {
                return _terrainChunks[Coords].GetSingleTreePosition();
            }
            return Vector3.zero;
        }
    }
}