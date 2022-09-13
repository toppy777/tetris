using UniRx;
using System;
using Tetris.Scripts.Domains.MinoTypes;

namespace Tetris.Scripts.Domains.Minos
{
    public class HoldMino
    {
        MinoType _minoType;
        bool _existFlg;

        Subject<MinoType> _whenSet = new Subject<MinoType>();
        public IObservable<MinoType> WhenSet => _whenSet;

        public bool Exists()
        {
            return _existFlg;
        }

        public MinoType GetMinoType()
        {
            return _minoType;
        }

        public void Set(MinoType minoType)
        {
            _existFlg = true;
            _minoType = minoType;
            _whenSet.OnNext(_minoType);
        }
    }
}
