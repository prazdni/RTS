using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class SelectedItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Slider _slider;

        public Sprite Icon { set => _icon.sprite = value; }
        public string Text { set => _text.text = value; }
        public float HealthPercent { set => _slider.value = value; }
    }
}