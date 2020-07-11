using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public class ItemController : CellContent<ItemResource>
    {
        private int currentSpriteIndex = 0;

        protected override void Start()
        {
            base.Start();
            if (GetResource().sprites.Length > 1)
            {
                InvokeRepeating("UpdateAnimatedSprite", 0f, 1f / GetResource().sprites.Length);
            }
        }

        void UpdateAnimatedSprite()
        {
            currentSpriteIndex = (currentSpriteIndex + 1) % GetResource().sprites.Length;
        }

        protected override Sprite GetCurrentSprite()
        {
            return GetResource().sprites[currentSpriteIndex];
        }
    }
}
