using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;

        public Button ContinueButton => _continueButton;
    }
}