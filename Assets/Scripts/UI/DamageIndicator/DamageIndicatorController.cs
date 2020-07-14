using TMPro;
using UnityEngine;

namespace UI.DamageIndicator
{
    public class DamageIndicatorController : MonoBehaviour
    {
        public Transform canvas;
        public GameObject damageTextPrefab;

        public void DisplayDamage(int damage)
        {
            var damageTextComponent = Instantiate(damageTextPrefab, canvas);
            var textController = damageTextComponent.GetComponent<TextMeshProUGUI>();
            textController.text = damage.ToString();
            textController.color = Color.red;

            var animation = damageTextComponent.GetComponent<Animation>();
            animation.Play();
            Destroy(damageTextComponent, animation.clip.length);
        }
    }
}
