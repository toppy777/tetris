using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Minos;
using System.Linq;
using System;

namespace Tetris.Scripts.Domains.MinoSets
{
    public class MinoSet
    {
        List<Mino> _list;

        public MinoSet(List<Mino> list)
        {
            if (list.Count != 7) {
                throw new ArgumentOutOfRangeException($"list.Count({list.Count}) is not correct.");
            }

            _list = list;
            ArrangeRandom();
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        public MinoSet(MinoSet minoSet)
        {
            this._list = minoSet._list;
        }

        /// <summary>
        /// Minoリストをランダムの順に並び替え
        /// </summary>
        public void ArrangeRandom()
        {
            _list = _list.OrderBy(a => Guid.NewGuid()).ToList();
        }

        public Mino PopMino()
        {
            Mino mino = _list[0];
            _list.RemoveAt(0);
            return mino;
        }

        public bool IsEmpty() {
            return _list.Count == 0;
        }

        public List<Mino> GetMinoSet()
        {
            return _list;
        }

        public Mino GetMinoAt(int index)
        {
            return _list[index];
        }
    }
}
