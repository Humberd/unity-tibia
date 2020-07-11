using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Resource/Item", order = 1)]
    public class ItemResource: Resource
    {
        public Texture2D texture;
        public bool isDraggable;

        [HideInInspector] public Sprite[] sprites;

        public override void LoadSprites()
        {
            sprites = Resources.LoadAll<Sprite>($"Sprites/Items/{texture.name}");
        }

        public override string GetLayerName()
        {
            return "Items";
        }
    }
}
