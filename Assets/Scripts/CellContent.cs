using System;
using ResourceTypes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class CellContent<TResourceType> : MonoBehaviour where TResourceType : Resource
{
    protected SpriteRenderer SpriteRenderer;
    private TResourceType _resource;
    private int _sortOrder;
    public Cell ParentCell { get; set; }
    public Vector3 BaseLocalPosition;
    public Vector3 LocalPositionOffset;

    private void Start()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        SpriteRenderer.sortingOrder = _sortOrder;
        SpriteRenderer.sprite = GetCurrentSprite();
        SpriteRenderer.sortingLayerName = _resource.GetLayerName();
        transform.localScale = new Vector2(_resource.scale, _resource.scale);
        float positionOffset = (_resource.scale - 1) / (float) 2;
        BaseLocalPosition = new Vector3(-positionOffset, positionOffset);
        transform.localPosition = BaseLocalPosition + LocalPositionOffset;
    }

    protected abstract Sprite GetCurrentSprite();

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
