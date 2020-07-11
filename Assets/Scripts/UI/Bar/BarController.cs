using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class BarController : MonoBehaviour
    {
        public Image barImage;
        public Canvas barCanvas;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = barCanvas.GetComponent<RectTransform>();
        }

        public void UpdateValue(float percent)
        {
            barImage.fillAmount = percent;
        }

        public void UpdatePosition()
        {
            if (_rectTransform == null)
            {
                Invoke("_updatePositionInternal", 0f);
                return;
            }

            _updatePositionInternal();
        }

        private void _updatePositionInternal()
        {
            transform.localPosition = new Vector2(0, (MyGrid.Instance.cellSize / 2f) + (_rectTransform.rect.height *2 / 2f));

        }
    }
}
