using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace Tetris.Scripts.Presenters.FinishCanvas
{
    public class BackToTitleButtonView : MonoBehaviour
    {
        Subject<Unit> _whenBackToTitle = new();
        public IObservable<Unit> WhenBackToTitle => _whenBackToTitle;

        public BackToTitleButtonView()
        {
            GetComponent<Button>().onClick.AddListener(() => {
                _whenBackToTitle.OnNext(Unit.Default);
            });
        }
    }
}
