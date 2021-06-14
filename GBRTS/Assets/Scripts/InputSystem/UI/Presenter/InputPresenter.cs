using System.Linq;
using Abstractions;
using InputSystem.UI.Model;
using UI.Model;
using UniRx;
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
        
        [Inject]
        protected void Init()
        {
            var nonBlockedByUiFramesStream = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());
            
            var leftClicksStream = nonBlockedByUiFramesStream.Where(_ => Input.GetMouseButtonDown(0));
            var rightClicksStream = nonBlockedByUiFramesStream.Where(_ => Input.GetMouseButtonDown(1));
            
            var leftClicksRaysStream = leftClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
            var rightClicksRaysStream = rightClicksStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

            var leftClicksRaysHitsStream = leftClicksRaysStream.Select(ray => Physics.RaycastAll(ray));
            var rightClicksRaysHitsStream = rightClicksRaysStream.Select(ray => (ray, Physics.RaycastAll(ray)));
            
            leftClicksRaysHitsStream.Subscribe(hits =>
            {
                if (isHit<ISelectableItem>(hits, out var selectableItem))
                {
                    if (isHit<IFractionMember>(hits, out var fractionMember))
                    {
                        if (fractionMember.Id != 0)
                        {
                            return;
                        }
                    }
                    
                    _currentSelection.SetValue(selectableItem);
                }
            });

            rightClicksRaysHitsStream.Subscribe((ray, hits) =>
            {
                if (isHit<IFractionMember>(hits, out var fractionMember))
                {
                    if (fractionMember.Id != 0)
                    {
                        if (isHit<IAttackable>(hits, out var attackableItem))
                        {
                            _currentAttackableValue.SetValue(attackableItem);
                        }
                    }
                }
                else if (Physics.Raycast(ray, out var hitInfo))
                {
                    _currentGroundPosition.SetValue(hitInfo.point);
                }
            });
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
