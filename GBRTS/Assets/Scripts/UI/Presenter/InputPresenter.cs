using Abstractions;
using UI.Model;
using UnityEngine;

namespace InputSystem
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectedItem _currentSelection;
        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
                {
                    var selectableItem = hitInfo.collider.GetComponent<ISelectableItem>();
                    _currentSelection.SetValue(selectableItem);
                }
                else
                {
                    _currentSelection.SetValue(null);
                }
            }
        }
    }
}
