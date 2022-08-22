using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace Tetris.Scripts.Domains.Minos
{
    public class Timer : ITickable
    {
        int _seconds;
        Subject<int> _whenASecondPassed;
        public IObservable<int> WhenASecondPassed => _whenASecondPassed;

        public Timer()
        {
            _whenASecondPassed = new Subject<int>();
        }

        void ITickable.Tick()
        {
            _whenASecondPassed.OnNext(_seconds);

            _seconds++;
        }
    }
}
