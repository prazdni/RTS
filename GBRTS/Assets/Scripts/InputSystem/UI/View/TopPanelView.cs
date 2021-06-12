using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class TopPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _time;
        [SerializeField] private Button menuButton;

        public int Time
        {
            set
            {
                _time.text = TimeSpan.FromSeconds(value).ToString();
            }
        }

        public Button MenuButton => menuButton;
    }
}