using Sprites;
using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Resource/Creature", order = 2)]
    public class CreatureResource : Resource
    {
        public DirectionalSprite directionalSprite;
        public float movementSpeed = 1.0f;

        public override string GetLayerName()
        {
            return "Creatures";
        }
    }
}
