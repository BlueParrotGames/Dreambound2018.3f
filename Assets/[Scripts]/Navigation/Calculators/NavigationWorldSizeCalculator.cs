using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Calculators
{
    public class NavigationWorldSizeCalculator
    {
        public static Vector3 GetWorldSize(MeshFilter[] meshFilters)
        {
            Vector3 result = Vector3.zero;

            for (int i = 0; i < meshFilters.Length; i++)
            {
                for (int v = 0; v < meshFilters[i].sharedMesh.vertices.Length; v++)
                {
                    Vector3 vertexWorldPosition = meshFilters[i].sharedMesh.vertices[v];
                    vertexWorldPosition.x *= meshFilters[i].transform.lossyScale.x; vertexWorldPosition.x += meshFilters[i].transform.position.x; //Set X-axis
                    vertexWorldPosition.y *= meshFilters[i].transform.lossyScale.y; vertexWorldPosition.y += meshFilters[i].transform.position.y; //Set Y-axis
                    vertexWorldPosition.z *= meshFilters[i].transform.lossyScale.z; vertexWorldPosition.z += meshFilters[i].transform.position.z; //Set Z-axis

                    //X-axis
                    if (Mathf.Abs(vertexWorldPosition.x) > Mathf.Abs(result.x))
                        result.x = Mathf.Abs(vertexWorldPosition.x * 2f);

                    //Y-axis
                    if (Mathf.Abs(vertexWorldPosition.y) > Mathf.Abs(result.y))
                        result.y = Mathf.Abs(vertexWorldPosition.y * 2f);

                    //Z-axis
                    if (Mathf.Abs(vertexWorldPosition.z) > Mathf.Abs(result.z))
                        result.z = Mathf.Abs(vertexWorldPosition.z * 2f);
                }
            }

            return result;
        }
    }
}
