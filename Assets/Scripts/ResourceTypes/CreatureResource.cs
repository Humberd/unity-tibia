using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Resource/Creature", order = 2)]
    public class CreatureResource : Resource
    {
        public float movementSpeed = 1.0f;

        public override string GetLayerName()
        {
            return "Creatures";
        }
    }
}
