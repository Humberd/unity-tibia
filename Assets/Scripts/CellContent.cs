using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellContent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Resource _resource;
    private int _sortOrder;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sortingOrder = _sortOrder;
        _spriteRenderer.sprite = _resource.sprite;
        _spriteRenderer.sortingLayerName = _resource.resourceType.toLayerName();
    }

    public void DestroyContent()
    {
        DestroyImmediate(gameObject);
    }

    public void SetResource(Resource resource)
    {
        _resource = resource;
    }

    public Resource GetResource()
    {
        return _resource;
    }

    public void SetOrder(int order)
    {
        _sortOrder = order;
    }
}
