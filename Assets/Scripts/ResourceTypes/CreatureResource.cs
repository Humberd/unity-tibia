using UnityEngine;

namespace ResourceTypes
{
    [CreateAssetMenu(fileName = "Creature", menuName = "Resource/Creature", order = 2)]
    public class CreatureResource : Resource
    {
        public Texture2D idle;
        public Texture2D walking;
        public float movementSpeed = 1f;
        public int maxHealth = 100;
        public int attackRange = 1;
        public int attackDamage = 10;
        public float attackPerSecond = 1f;

        public WalkAnimations walkAnimations;
        public IdleAnimations idleAnimations;

        public override void LoadSprites()
        {
            var idleSprites = Resources.LoadAll<Sprite>($"Sprites/Creatures/{idle.name}");
            idleAnimations.Up = idleSprites[0];
            idleAnimations.Right = idleSprites[1];
            idleAnimations.Down = idleSprites[2];
            idleAnimations.Left = idleSprites[3];

            var walkingSprites = Resources.LoadAll<Sprite>($"Sprites/Creatures/{walking.name}");

            var animationsCount = walkingSprites.Length / 4;
            walkAnimations.Up = new Sprite[animationsCount];
            walkAnimations.Right = new Sprite[animationsCount];
            walkAnimations.Down = new Sprite[animationsCount];
            walkAnimations.Left = new Sprite[animationsCount];

            for (var i = 0; i < walkingSprites.Length; i++)
            {
                var whole = i / 4;
                var remainder = i % 4;
                if (remainder == 0)
                {
                    walkAnimations.Up[whole] = walkingSprites[i];
                }
                else if (remainder == 1)
                {
                    walkAnimations.Right[whole] = walkingSprites[i];
                }
                else if (remainder == 2)
                {
                    walkAnimations.Down[whole] = walkingSprites[i];
                }
                else if (remainder == 3)
                {
                    walkAnimations.Left[whole] = walkingSprites[i];
                }
            }
        }

        public override string GetLayerName()
        {
            return "Creatures";
        }

        public struct WalkAnimations
        {
            public Sprite[] Up;
            public Sprite[] Right;
            public Sprite[] Down;
            public Sprite[] Left;
        }

        public struct IdleAnimations
        {
            public Sprite Up;
            public Sprite Right;
            public Sprite Down;
            public Sprite Left;
        }
    }
}
