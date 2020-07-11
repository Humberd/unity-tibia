using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Terrain", menuName = "Resource/Terrain", order = 0)]
    public class TerrainResource : Resource
    {
        public Texture2D texturePool;
        public bool isWalkable;

        [HideInInspector] public Sprite[] randomTextures;

        public override void LoadSprites()
        {
            randomTextures = Resources.LoadAll<Sprite>($"Sprites/Terrain/{texturePool.name}");
        }

        public override string GetLayerName()
        {
            return "Terrain";
        }
    }
}
