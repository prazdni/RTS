using Core;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace InputSystem.UI.Presenter
{
    public class MenuPresenter : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;
        
        private ITimeModel _timeModel;

        [Inject]
        public void Init(ITimeModel timeModel)
        {
            _timeModel = timeModel;
            _menuView.ContinueButton.OnClickAsObservable().Subscribe(unit => HandleContinueButtonClick());
        }

        private void HandleContinueButtonClick()
        {
            gameObject.SetActive(false);
            _timeModel.SetPause(false);
        }
    }
}