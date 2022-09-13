using System.Collections.Generic;
using Tetris.Scripts.Domains.MinoTypes;
using System;
using System.Linq;

namespace Tetris.Scripts.Domains.MinoReserves
{
    public class MinoReserve
    {
        List<MinoType> _list;

        public int Count {
            get { return _list.Count; }
        }

        public MinoReserve()
        {
            _list = new List<MinoType>();
            _list.Add(MinoType.I);
            _list.Add(MinoType.O);
            _list.Add(MinoType.S);
            _list.Add(MinoType.Z);
            _list.Add(MinoType.J);
            _list.Add(MinoType.L);
            _list.Add(MinoType.T);
            ArrangeRandom();
        }

        /// <summary>
        /// Minoリストをランダムの順に並び替え
        /// </summary>
        public void ArrangeRandom()
        {
            _list = _list.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public MinoType PopMinoType()
        {
            MinoType mino = _list[0];
            _list.RemoveAt(0);
            return mino;
        }

        public bool IsEmpty() {
            return _list.Count == 0;
        }

        public List<MinoType> GetMinoTypeList()
        {
            return _list;
        }

        public MinoType GetMinoTypeAt(int index)
        {
            return _list[index];
        }
    }
}
