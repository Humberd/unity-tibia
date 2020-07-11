using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public class TerrainController : CellContent<TerrainResource>
    {
        protected override void Start()
        {
            base.Start();
            SpriteRenderingController.UpdateSprite(RandomSprite());
        }

        private Sprite RandomSprite()
        {
            return GetResource().randomTextures[Random.Range(0, GetResource().randomTextures.Length - 1)];
        }

    }
}
