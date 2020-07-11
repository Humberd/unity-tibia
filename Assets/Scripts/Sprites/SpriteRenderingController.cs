using System;
using UnityEngine;

namespace Sprites
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRenderingController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Sprite _sprite;
        private int _sortOrder;
        private string _layerName;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _spriteRenderer.sprite = _sprite;
            _spriteRenderer.sortingOrder = _sortOrder;
            _spriteRenderer.sortingLayerName = _layerName;
        }

        public void UpdateSprite(Sprite sprite)
        {
            _sprite = sprite;
        }

        public void UpdateSortOrder(int sortOrder)
        {
            _sortOrder = sortOrder;
        }

        public void UpdateLayerName(string layerName)
        {
            _layerName = layerName;
        }

        public void UpdateScale(float scale)
        {
            transform.localScale = new Vector2(scale, scale);
        }
    }
}
