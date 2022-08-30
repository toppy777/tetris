using UnityEngine;
using System.Collections.Generic;
using Tetris.Scripts.Domains.Minos;
using System.Linq;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.MinoSets
{
    public class MinoSetList
    {
        List<MinoSet> _list = new();

        Subject<Unit> _whenMinoSetRemove;
        public IObservable<Unit> WhenMinoSetRemove => _whenMinoSetRemove;

        public MinoSetList(
            List<Mino> list1,
            List<Mino> list2
        ) {
            _list.Add(new MinoSet(list1));
            _list.Add(new MinoSet(list2));
            _whenMinoSetRemove = new Subject<Unit>();
        }

        /// <summary>
        /// MinoSetから先頭のMinoを取り出す
        /// </summary>
        public Mino PopMino()
        {
            Mino mino = _list[0].PopMino();

            // MinoSetが空になったら、削除して新規追加
            if (_list[0].IsEmpty()) {
                _list.RemoveAt(0);
                // 追加願い通知
                _whenMinoSetRemove.OnNext(Unit.Default);
            }

            return mino;
        }

        /// <summary>
        /// 次のミノ7つをリストにして返す
        /// </summary>
        /// <returns></returns>
        public List<Mino> GetNextMinoSet()
        {
            var list = _list[0].GetMinoSet();

            if (list.Count != 7) {
                for (int i = 0; i < 7 - list.Count; i++) {
                    list.Add(_list[1].GetMinoAt(i));
                }
            }

            return list;
        }

        public void AddMinoSet(List<Mino> list)
        {
            _list.Add(new MinoSet(list));
        }
    }
}
