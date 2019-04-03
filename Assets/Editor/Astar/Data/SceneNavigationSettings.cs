using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astar.Editor.Data
{
    public class SceneNavigationSettings : ScriptableObject
    {
        public LayerMask UnwalkableMask;
        public Vector3 GridWorldSize;

        public void UpdateValues(LayerMask unwalkableMask, Vector3 gridWorldSize)
        {
            UnwalkableMask = unwalkableMask;
            GridWorldSize = gridWorldSize;
        }
    }
}
