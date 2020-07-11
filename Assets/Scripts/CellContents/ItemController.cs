using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public class ItemController : CellContent<ItemResource>
    {
        private int _currentSpriteIndex = 0;

        protected override void Start()
        {
            base.Start();
            if (GetResource().sprites.Length > 1)
            {
                InvokeRepeating("UpdateAnimatedSprite", 0f, 1f / GetResource().sprites.Length);
            }
            else
            {
                SpriteRenderingController.UpdateSprite(GetResource().sprites[0]);
            }
        }

        void UpdateAnimatedSprite()
        {
            _currentSpriteIndex = (_currentSpriteIndex + 1) % GetResource().sprites.Length;
            SpriteRenderingController.UpdateSprite(GetResource().sprites[_currentSpriteIndex]);
        }
    }
}
