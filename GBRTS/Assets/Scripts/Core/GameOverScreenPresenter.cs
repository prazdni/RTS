using System.Text;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameOverScreenPresenter : MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                var stringBuilder = new StringBuilder($"Game Over!");
                stringBuilder.AppendLine(result == 0 ? "Draw!" : $"Win fraction №{result}");
                _view.SetActive(true);
                _text.text = stringBuilder.ToString();
                Time.timeScale = 0;
            });
        }
    }
}