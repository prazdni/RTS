using System;
using System.Threading;
using Abstractions;
using UniRx;
using UnityEngine;

namespace Core
{
    public class GameStatus : MonoBehaviour, IGameStatus
    {
        public IObservable<int> Status => _status;
        private Subject<int> _status = new Subject<int>();

        private void CheckStatus(object state)
        {
            if (FractionMember.FractionsCount == 0)
                _status.OnNext(0);
            else if (FractionMember.FractionsCount == 1)
                _status.OnNext(FractionMember.GetWinner());
        }

        private void Update()
        {
            ThreadPool.QueueUserWorkItem(CheckStatus);
        }
    }
}