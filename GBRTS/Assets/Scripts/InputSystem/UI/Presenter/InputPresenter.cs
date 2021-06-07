using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InputSystem
{
    public class InputPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EventSystem _eventSystem;
        [Inject] private SelectedItem _currentSelection;
        [Inject] private Vector3Value _currentGroundPosition;
        [Inject] private AttackableValue _currentAttackableValue;
        
        protected void Update()
        {
            if (_eventSystem.IsPointerOverGameObject())
                return;

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

            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hitInfo))
                {
                    _currentGroundPosition.SetValue(hitInfo.point);
                }
            }
        }

        private bool isHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = null;

            if (hits.Length == 0)
                return false;
            
            result = hits.Select(hit => hit.collider.GetComponentInParent<T>()).FirstOrDefault(c => c != null);
            return result != null;
        }
    }
}
