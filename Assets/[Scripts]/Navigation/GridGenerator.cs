using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

using Astar.Data;

namespace Astar.Generators
{
    public class GridGenerator
    {
        private const int ObstacleProximityPenalty = 100;

        public static Node[,,] GenerateGrid(SceneNavigationSettings settings, TerrainTypeAsset terrainTypes)
        {
            int blurSize = 2;
            Node[,,] grid = new Node[settings.GridSize.x, settings.GridSize.y, settings.GridSize.z];

            for (int x = 0; x < settings.GridSize.x; x++)
            {
                for (int y = 0; y < settings.GridSize.y; y++)
                {
                    for (int z = 0; z < settings.GridSize.z; z++)
                    {
                        //Calculate the worldpoint of the current node
                        Vector3 worldPoint = settings.WorldBottomBackLeft +
                            Vector3.right * (x * settings.NodeDiameter + settings.NodeRadius) +
                            Vector3.up * (y * settings.NodeDiameter + settings.NodeRadius) +
                            Vector3.forward * (z * settings.NodeDiameter + settings.NodeRadius);

                        //If this is a useless floating node, set it to NULL and go on to the next itteration
                        if (!Physics.CheckSphere(worldPoint, settings.NodeRadius))
                        {
                            grid[x, y, z] = null;
                            continue;
                        }

                        //Check if the node is within or too close to an obstacle
                        //If so set the node to non-walkable
                        bool walkable = !Physics.CheckSphere(worldPoint, settings.NodeRadius, terrainTypes.WalkableMask, QueryTriggerInteraction.Ignore);

                        //If the node is originaly walkable check if the passage way isn't too low for the agent
                        //If it is, set walkable to false
                        if (walkable)
                        {
                            if (Physics.Raycast(worldPoint, Vector3.up * settings.AgentHeight, out RaycastHit hitUp))
                            {
                                if (Physics.Raycast(worldPoint, Vector3.down * settings.AgentHeight, out RaycastHit hitDown))
                                {
                                    walkable = !(hitUp.point.y - hitDown.point.y < settings.AgentHeight);
                                }
                            }
                        }

                        bool groundNode = false;
                        Ray groundRay = new Ray(worldPoint, Vector3.down);
                        Vector3 groundPosition = Vector3.zero;
                        if (Physics.Raycast(groundRay, out RaycastHit groundHit, settings.NodeDiameter))
                        {
                            groundNode = true;
                            groundPosition = groundHit.point;
                        }

                        //Find movement penalty
                        int movementPenalty = 0;
                        if (walkable)
                        {
                            Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                            if (Physics.Raycast(ray, out RaycastHit penaltyHit, 100, terrainTypes.WalkableMask))
                            {
                                terrainTypes.WalkableRegionsDictionary.TryGetValue(penaltyHit.collider.gameObject.layer, out movementPenalty);
                            }
                        }
                        else if (!walkable)
                        {
                            movementPenalty += ObstacleProximityPenalty;
                        }

                        grid[x, y, z] = new Node(walkable, worldPoint, groundPosition, new Vector3Int(x, y, z), movementPenalty, groundNode);
                    }
                }
            }

            Node[,,] writeGrid = grid;
            int kernelSize = blurSize * 2 - 1;
            int kernelExtends = (kernelSize - 1) / 2;

            for (int y = 0; y < settings.GridSize.y; y++)
            {
                for (int x = 0; x < settings.GridSize.x; x++)
                {
                    for (int z = 0; z < settings.GridSize.z; z++)
                    {
                        if (grid[x, y, z] != null)
                        {
                            int totalPenalty = 0;
                            for (int rangeX = -kernelExtends; rangeX <= kernelExtends; rangeX++)
                            {
                                for (int rangeY = -kernelExtends; rangeY <= kernelExtends; rangeY++)
                                {
                                    for (int rangeZ = -kernelExtends; rangeZ <= kernelExtends; rangeZ++)
                                    {
                                        int sampleX = Mathf.Clamp(rangeX + x, 0, settings.GridSize.x - 1);
                                        int sampleY = Mathf.Clamp(rangeY + y, 0, settings.GridSize.y - 1);
                                        int sampleZ = Mathf.Clamp(rangeZ + z, 0, settings.GridSize.z - 1);

                                        if (grid[sampleX, sampleY, sampleZ] != null)
                                            totalPenalty += grid[sampleX, sampleY, sampleZ].MovementPenalty;

                                        int blurredPenalty = Mathf.RoundToInt((float)totalPenalty / (kernelSize * kernelSize * kernelSize));
                                        writeGrid[x, y, z].MovementPenalty = blurredPenalty;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Debug.LogWarning(grid.Length);

            return writeGrid;
        }
    }
}
