using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellContent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Resource _resource;

    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (_resource)
        {
            _spriteRenderer.sprite = _resource.sprite;
            _spriteRenderer.sortingLayerName = _resource.resourceType.toLayerName();
        }
    }

    public void DestroyContent()
    {
        DestroyImmediate(gameObject);
    }

    public void SetResource(Resource resource)
    {
        _resource = resource;
        if (_spriteRenderer)
        {
            _spriteRenderer.sprite = resource.sprite;
            _spriteRenderer.sortingLayerName = resource.resourceType.toLayerName();
        }
    }
}
