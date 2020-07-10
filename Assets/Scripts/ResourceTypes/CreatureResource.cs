using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Resource/Creature", order = 2)]
    public class CreatureResource : Resource
    {
        public override string GetLayerName()
        {
            return "Creatures";
        }
    }
}
