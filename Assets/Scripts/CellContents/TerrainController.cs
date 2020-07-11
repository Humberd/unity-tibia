using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public class TerrainController : CellContent<TerrainResource>
    {
        private Sprite _randomSprite;
        protected override Sprite GetCurrentSprite()
        {
            if (!_randomSprite)
            {
                _randomSprite = GetResource().randomTextures[Random.Range(0, GetResource().randomTextures.Length - 1)];
            }

            return _randomSprite;
        }
    }
}
