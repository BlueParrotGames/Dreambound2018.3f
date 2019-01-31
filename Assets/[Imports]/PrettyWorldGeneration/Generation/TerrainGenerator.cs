using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnvironmentGeneration;

namespace EnvironmentGeneration
{
    public class TerrainGenerator : MonoBehaviour
    {
        const float viewerMoveThresholdForChunkUpdate = 25f;
        const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;

        public int colliderLODIndex;
        public LODInfo[] detailLevels;

        [SerializeField] private Vector2 _terrainSizeInChunks;
        [SerializeField] private bool _infinteWorld = false;

        public MeshSettings meshSettings;
        public HeightMapSettings heightMapSettings;
        public TextureData textureSettings;

        public Transform viewer;
        public Material mapMaterial;

        Vector2 viewerPosition;
        Vector2 viewerPositionOld;

        float meshChunksize;
        int chunksVisibleInViewDst;

        Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
        List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();

        //[Header("Tree Spawning")]
        //[SerializeField]
        //private GameObject _treePrefab;

        //map values
        private float _totalMapHeight;
        private float _heighestTerrainPoint;
        private float _lowestTerrainPoint;
        private Transform[] _chunkObjects;

        private EnvironmentGenerator _environmentGenerator;

        void Start()
        {
            textureSettings.ApplyToMaterial(mapMaterial);
            textureSettings.UpdateMeshHeights(mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);

            float maxViewDst = detailLevels[detailLevels.Length - 1].visibleDstThreshold;
            meshChunksize = meshSettings.meshWorldSize;
            chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / meshChunksize);

            //retrieve the values needed to generate things
            _heighestTerrainPoint = HeightMapGenerator.GetHeighestPoint(GetWorldHeightMap());
            _lowestTerrainPoint = HeightMapGenerator.GetLowestPoint(GetWorldHeightMap());

            _environmentGenerator = new EnvironmentGenerator(GetWorldHeightMap(), meshSettings, textureSettings);

            //Assign all the chunks
            int minX = -Mathf.RoundToInt(_terrainSizeInChunks.x * 0.5f);
            int maxX = Mathf.RoundToInt(_terrainSizeInChunks.x * 0.5f);
            int minY = -Mathf.RoundToInt(_terrainSizeInChunks.y * 0.5f);
            int maxY = Mathf.RoundToInt(_terrainSizeInChunks.y * 0.5f);
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Vector2 chunkCoord = new Vector2(x, y);

                    TerrainChunk newChunk = new TerrainChunk(chunkCoord, heightMapSettings, meshSettings, textureSettings, detailLevels, colliderLODIndex, transform, viewer, mapMaterial, 11);
                    terrainChunkDictionary.Add(chunkCoord, newChunk);
                    newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
                    newChunk.Load();

                    //Debug.Log(chunkCoord);

                    _environmentGenerator.RegisterChunk(chunkCoord, GetChunkHeightMap(chunkCoord)); //register this chunk to the EnvironmentGenerator
                }
            }

            UpdateVisibleChunks();

            GetAllChunksTransforms();

            // ----->>> SpawnMultipleTrees();
        }

        private void GetAllChunksTransforms()
        {
            _chunkObjects = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _chunkObjects[i] = transform.GetChild(i).transform;
            }
        }
        private HeightMap GetWorldHeightMap()
        {
            float mapWidth = meshSettings.numVertsPerLine * _terrainSizeInChunks.x;
            float mapHeight = meshSettings.numVertsPerLine * _terrainSizeInChunks.y;
            Vector2 sampleCenter = Vector2.zero * meshSettings.meshWorldSize / meshSettings.meshScale;

            return HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, sampleCenter);
        }
        private HeightMap GetChunkHeightMap(Vector2 ChunkCoord)
        {
            Vector2 sampleCenter = ChunkCoord * meshSettings.meshWorldSize / meshSettings.meshScale;
            return HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, sampleCenter);
        }

        //#region Tree Generation
        //private void SpawnMultipleTrees(int TreeCount, Vector2 chunkCoords)
        //{
        //    for (int i = 0; i < TreeCount; i++)
        //    {
        //        SpawnSingleTree(chunkCoords, i);
        //    }
        //}
        //private void SpawnSingleTree(Vector2 chunkCoord)
        //{
        //    int width = (int)Mathf.Sqrt(_chunkObjects.Length);
        //    Transform chunk = _chunkObjects[(int)chunkCoord.x * width + (int)chunkCoord.y];

        //    Vector3 spawnPos = _environmentGenerator.GetChunkTree(chunkCoord);

        //    GameObject Tree = GameObject.Instantiate(_treePrefab, spawnPos, Quaternion.identity, chunk);
        //}
        //private void SpawnSingleTree(Vector2 chunkCoord, int index)
        //{
        //    int width = Mathf.RoundToInt(Mathf.Sqrt(_chunkObjects.Length));
        //    int parentIndex = (int)(chunkCoord.x) * width + (int)chunkCoord.y;

        //    Transform chunk = _chunkObjects[parentIndex];

        //    Vector3 spawnPos = _environmentGenerator.GetChunkTree(chunkCoord);
        //    float ChunkSize = (meshSettings.numVertsPerLine * meshSettings.meshScale) * 0.9433962264150943f; // get the chunk size in units

        //    spawnPos.x += ChunkSize * chunkCoord.x;
        //    spawnPos.z += ChunkSize * chunkCoord.y;

        //    GameObject Tree = GameObject.Instantiate(_treePrefab, spawnPos, Quaternion.identity);
        //    Tree.transform.name = "Tree (" + index + ")";
        //}
        //#endregion

        void Update()
        {
            viewerPosition = new Vector2(viewer.position.x, viewer.position.z);

            if (viewerPosition != viewerPositionOld)
            {
                foreach (TerrainChunk chunk in visibleTerrainChunks)
                {
                    chunk.UpdateCollisionMesh();
                }
            }

            if ((viewerPositionOld - viewerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate)
            {
                viewerPositionOld = viewerPosition;
                UpdateVisibleChunks();
            }


        }

        void UpdateVisibleChunks()
        {
            HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
            for (int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
            {
                alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
                visibleTerrainChunks[i].UpdateTerrainChunk();
            }

            int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / meshChunksize);
            int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / meshChunksize);

            for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++)
            {
                for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++)
                {
                    Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                    if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
                    {
                        if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                        {
                            terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                        }
                        else
                        {

                            //TerrainChunk newChunk = new TerrainChunk(viewedChunkCoord, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, transform, viewer, mapMaterial);
                            //terrainChunkDictionary.Add(viewedChunkCoord, newChunk);
                            //newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
                            //newChunk.Load();

                        }
                    }
                }
            }
        }

        void OnTerrainChunkVisibilityChanged(TerrainChunk chunk, bool isVisible)
        {
            if (isVisible)
            {
                visibleTerrainChunks.Add(chunk);
            }
            else
            {
                visibleTerrainChunks.Remove(chunk);
            }
        }
    }

    [System.Serializable]
    public struct LODInfo
    {

        [Range(0, MeshSettings.numSupportedLODs - 1)]
        public int lod;
        public float visibleDstThreshold;


        public float sqrVisibleDstThreshold
        {
            get
            {
                return visibleDstThreshold * visibleDstThreshold;
            }
        }
    }

}