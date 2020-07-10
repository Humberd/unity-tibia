using System;
using UnityEngine;

namespace Sprites
{
    [Serializable]
    public class DirectionalSprite : CellSpriteRenderer
    {
        public Sprite up;
        public Sprite down;
        public Sprite left;
        public Sprite right;
    }
}
