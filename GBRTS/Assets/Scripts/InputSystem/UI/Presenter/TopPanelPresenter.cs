using Core;
using UI.View;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace InputSystem.UI.Presenter
{
    public class TopPanelPresenter : MonoBehaviour
    {
        [SerializeField] private TopPanelView _topPanelView;
        [SerializeField] private GameObject _menu;

        private ITimeModel _timeModel;
            
        [Inject]
        public void Init(ITimeModel timeModel)
        {
            _timeModel = timeModel;
            timeModel.GameTime.Subscribe(value => _topPanelView.Time = value);
            _topPanelView.MenuButton.OnClickAsObservable().Subscribe(unit => HandleMenuButtonClick());
        }

        private void HandleMenuButtonClick()
        {
            _menu.SetActive(true);
            _timeModel.SetPause(true);
        }
    }
}