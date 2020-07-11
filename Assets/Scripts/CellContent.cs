using ResourceTypes;
using Sprites;
using UnityEngine;

public abstract class CellContent<TResourceType> : MonoBehaviour where TResourceType : Resource
{
    protected SpriteRenderingController SpriteRenderingController;
    private TResourceType _resource;
    public Cell ParentCell { get; set; }
    public Vector3 BaseLocalPosition;
    public Vector3 LocalPositionOffset;
    private int delayedSortOrder;

    protected virtual void Start()
    {
        var spriteRendererObject = new GameObject("SpriteRenderingControllerObject");
        spriteRendererObject.transform.SetParent(transform);
        SpriteRenderingController = spriteRendererObject.AddComponent<SpriteRenderingController>();

        if (delayedSortOrder != 0)
        {
            SpriteRenderingController.UpdateSortOrder(delayedSortOrder);
        }

        SpriteRenderingController.UpdateLayerName(_resource.GetLayerName());
    }

    protected virtual void Update()
    {
        transform.localScale = new Vector2(_resource.scale, _resource.scale);
        float positionOffset = (_resource.scale - 1) / (float) 2;
        BaseLocalPosition = new Vector3(-positionOffset, positionOffset);
        transform.localPosition = BaseLocalPosition + LocalPositionOffset;
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
        if (SpriteRenderingController)
        {
            SpriteRenderingController.UpdateSortOrder(order);

        }
    }
}
