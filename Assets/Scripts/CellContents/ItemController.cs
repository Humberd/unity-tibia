using ResourceTypes;
using UnityEngine;

namespace CellContents
{
    public class ItemController : CellContent<ItemResource>
    {

        protected override Sprite GetCurrentSprite()
        {
            return GetResource().sprite;
        }
    }
}
