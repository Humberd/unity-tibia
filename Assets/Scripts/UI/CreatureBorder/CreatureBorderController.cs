using UnityEngine;

namespace UI.CreatureBorder
{
    public class CreatureBorderController : MonoBehaviour
    {
        public Transform mask;
        public SpriteRenderer spriteRenderer;


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetAttackMode()
        {
            spriteRenderer.color = Color.black;
        }
    }
}
