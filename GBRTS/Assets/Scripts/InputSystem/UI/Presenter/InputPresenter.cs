using Abstractions;
using UI.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectedItem _currentSelection;
        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_eventSystem.IsPointerOverGameObject())
                    return;
                
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
