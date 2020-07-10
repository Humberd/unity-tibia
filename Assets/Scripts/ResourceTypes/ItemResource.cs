using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Item", menuName = "Resource/Item", order = 1)]
    public class ItemResource: Resource
    {
        public bool isDraggable;

        public override string GetLayerName()
        {
            return "Items";
        }
    }
}
