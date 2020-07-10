using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Terrain", menuName = "Resource/Terrain", order = 0)]
    public class TerrainResource : Resource
    {
        public bool isWalkable;

        public override string GetLayerName()
        {
            return "Terrain";
        }
    }
}
