using ResourceTypes;
using Sprites;
using UnityEngine;

public abstract class CellContent<TResourceType> : MonoBehaviour where TResourceType : Resource
{
    protected SpriteRenderingController SpriteRenderingController;
    private TResourceType _resource;
    public Cell ParentCell { get; set; }
    public Vector3 animationPositionOffset;
    private int _lazySortOrder;

    protected virtual void Start()
    {
        var spriteRendererObject = new GameObject("SpriteRenderingControllerObject");
        spriteRendererObject.transform.SetParent(transform);
        SpriteRenderingController = spriteRendererObject.AddComponent<SpriteRenderingController>();

        SpriteRenderingController.UpdateLayerName(_resource.GetLayerName());
        SpriteRenderingController.UpdateSortOrder(_lazySortOrder);
        SpriteRenderingController.SetupTransforms(_resource.scale);
    }

    protected virtual void Update()
    {
        transform.localPosition = animationPositionOffset;
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
        if (!SpriteRenderingController)
        {
            _lazySortOrder = order;
            return;
        }
        SpriteRenderingController.UpdateSortOrder(order);
    }
}
