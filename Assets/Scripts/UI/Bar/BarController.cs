using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bar
{
    public class BarController : MonoBehaviour
    {
        public Image barImage;
        public Canvas barCanvas;
        public TextMeshProUGUI nameControl;
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = barCanvas.GetComponent<RectTransform>();
        }

        public void UpdateHealth(float percent)
        {
            barImage.fillAmount = percent;
            barImage.color = Color.green;
            nameControl.color = Color.green;
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

        public void UpdateName(string name)
        {
            StartCoroutine(_updateName(name));
        }

        private IEnumerator _updateName(string name)
        {
            yield return new WaitForSeconds(0f);
            nameControl.text = name;
        }
    }
}
