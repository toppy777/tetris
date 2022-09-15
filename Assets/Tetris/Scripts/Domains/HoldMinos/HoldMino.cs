using UniRx;
using System;
using Tetris.Scripts.Domains.MinoTypes;
using UnityEngine;

namespace Tetris.Scripts.Domains.HoldMinos
{
    public class HoldMino
    {
        MinoType _minoType;
        bool _exists = false;
        public bool Exists => _exists;
        bool _isAvailable = true;
        public bool IsAvailable => _isAvailable;
        bool _isFirst = true;
        public bool IsFirst => _isFirst;

        Subject<MinoType> _whenSet = new Subject<MinoType>();
        public IObservable<MinoType> WhenSet => _whenSet;

        public MinoType GetMinoType()
        {
            return _minoType;
        }

        public void Set(MinoType minoType)
        {
            _minoType = minoType;
            _isAvailable = false;
            _exists = true;
            _whenSet.OnNext(_minoType);
        }

        public void SetAvailable()
        {
            _isAvailable = true;
        }

        public void SetNotFirst()
        {
            _isFirst = false;
        }
    }
}
