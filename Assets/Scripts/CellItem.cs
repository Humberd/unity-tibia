using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellItem : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Item _item;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (_item)
        {
            _spriteRenderer.sprite = _item.sprite;
            _spriteRenderer.sortingLayerName = _item.itemType.toLayerName();
        }
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    public void SetItem(Item item)
    {
        _item = item;
        if (_spriteRenderer)
        {
            _spriteRenderer.sprite = item.sprite;
            _spriteRenderer.sortingLayerName = item.itemType.toLayerName();
        }
    }
}
