using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Tetris.Scripts.Presenters.FinishCanvas
{
    public class RestartButtonView : MonoBehaviour
    {
        Subject<Unit> _whenRestart = new();
        public IObservable<Unit> WhenRestart => _whenRestart;

        public RestartButtonView()
        {
            GetComponent<Button>().onClick.AddListener(() => {
                _whenRestart.OnNext(Unit.Default);
            });
        }
    }
}
