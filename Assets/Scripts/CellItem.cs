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
    }

    public void SetItem(Item item)
    {
        this._item = item;
        _spriteRenderer.sprite = item.sprite;
    }
}
