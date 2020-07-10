using System;
using ResourceTypes;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class CellContent<TResourceType> : MonoBehaviour where TResourceType : Resource
{
    private SpriteRenderer _spriteRenderer;
    private TResourceType _resource;
    private int _sortOrder;
    protected Cell ParentCell;
    protected Vector3 BaseLocalPosition;
    protected Vector3 LocalPositionOffset;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected void Update()
    {
        _spriteRenderer.sortingOrder = _sortOrder;
        _spriteRenderer.sprite = _resource.sprite;
        _spriteRenderer.sortingLayerName = _resource.GetLayerName();
        transform.localScale = new Vector2(_resource.scale, _resource.scale);
        float positionOffset = (_resource.scale - 1) / (float) 2;
        BaseLocalPosition = new Vector3(-positionOffset, positionOffset);
        transform.localPosition = BaseLocalPosition + LocalPositionOffset;
    }

    public void DestroyContent()
    {
        Destroy(gameObject);
    }

    public void SetParentCell(Cell parentCell)
    {
        ParentCell = parentCell;
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
