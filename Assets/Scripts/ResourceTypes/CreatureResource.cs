using System;
using Asserts;
using Sprites;
using UnityEngine;
using UnityEngine.U2D;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Resource/Creature", order = 2)]
    public class CreatureResource : Resource
    {
        public Texture2D idle;
        public Texture2D walking;
        public float movementSpeed = 1.0f;

        [HideInInspector] public Sprite[] idleSprites;
        [HideInInspector] public Sprite[] walkingSprites;

        public override void LoadSprites()
        {
            idleSprites = Resources.LoadAll<Sprite>($"Sprites/Creatures/{idle.name}");
            walkingSprites = Resources.LoadAll<Sprite>($"Sprites/Creatures/{walking.name}");
        }

        public override string GetLayerName()
        {
            return "Creatures";
        }
    }
}
