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

        public int GetWidth()
        {
            return (int) _rectTransform.rect.width;
        }
    }
}
