using System;
using ResourceTypes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class CellContent<TResourceType> : MonoBehaviour where TResourceType : Resource
{
    private SpriteRenderer _spriteRenderer;
    private TResourceType _resource;
    private int _sortOrder;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sortingOrder = _sortOrder;
        _spriteRenderer.sprite = _resource.sprite;
        _spriteRenderer.sortingLayerName = _resource.GetLayerName();
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }

    public void SetResource(TResourceType resource)
    {
        _resource = resource;
    }

    public TResourceType GetResource()
    {
        return _resource;
    }

    public void SetOrder(int order)
    {
        _sortOrder = order;
    }
}
