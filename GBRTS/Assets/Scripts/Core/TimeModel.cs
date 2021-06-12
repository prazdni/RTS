using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class TimeModel : ITimeModel, ITickable
    {
        public IObservable<int> GameTime => _gameTime.Select(value => (int)value);
        private bool _isPaused;
        
        public void SetPause(bool isPause)
        {
            _isPaused = isPause;
            Time.timeScale = isPause ? 0.0f : 1.0f;
        }

        private readonly ReactiveProperty<float> _gameTime = new ReactiveProperty<float>();
        
        public void Tick()
        {
            if (!_isPaused)
            {
                _gameTime.Value += Time.deltaTime;
            }
        }
    }
}