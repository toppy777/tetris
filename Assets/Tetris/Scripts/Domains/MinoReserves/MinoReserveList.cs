using System.Collections.Generic;
using Tetris.Scripts.Domains.MinoTypes;
using UniRx;
using System;

namespace Tetris.Scripts.Domains.MinoReserves
{
    public class MinoReserveList
    {
        List<MinoReserve> _list = new();

        // Subject<List<(List<Vector2Int>shape, Color32 color)>> _whenPop;
        // public IObservable<List<(List<Vector2Int>shape, Color32 color)>> WhenPop => _whenPop;

        Subject<Unit> _whenPop;
        public IObservable<Unit> WhenPop => _whenPop;

        public MinoReserveList()
        {
            _list.Add(new MinoReserve());
            _list.Add(new MinoReserve());
            // _whenPop = new Subject<List<(List<Vector2Int>shape, Color32 color)>>();
            _whenPop = new Subject<Unit>();
        }

        /// <summary>
        /// MinoSetから先頭のMinoTypeを取り出す
        /// </summary>
        public MinoType PopMinoType()
        {
            MinoType mino = _list[0].PopMinoType();

            // MinoSetが空になったら、削除して新規追加
            if (_list[0].IsEmpty()) {
                _list.RemoveAt(0);
                AddMinoReserve();
            }

            // 次の7つのミノの形と色を通知
            // List<(List<Vector2Int> ,Color32)> message = new List<(List<Vector2Int>, Color32)>();
            // List<MinoType> typeList = new List<MinoType>();
            // for (int i = 0; i < 7; i++) {
            //     message.Add((MinoShapeType.GetShape(GetMinoTypeAt(i))[0], MinoColor.GetColor(GetMinoTypeAt(i))));
            // }
            // _whenPop.OnNext(message);
            _whenPop.OnNext(Unit.Default);

            return mino;
        }

        /// <summary>
        /// 添え字を与えると、対応したMinoを取得する
        /// </summary>
        public MinoType GetMinoTypeAt(int index)
        {
            if (index >= _list[0].Count) {
                return _list[1].GetMinoTypeAt(index - _list[0].Count);
            }
            return _list[0].GetMinoTypeAt(index);
        }

        public MinoType GetLastMinoType()
        {
            if (_list[0].Count > 6) {
                return _list[0].GetMinoTypeAt(6);
            }
            return _list[1].GetMinoTypeAt(6 - _list[0].Count);
        }

        public List<MinoType> GetLastMinoTypes()
        {
            List<MinoType> typeList = new List<MinoType>();
            for (int i = 0; i < 7; i++) {
                typeList.Add(GetMinoTypeAt(i));
            }
            return typeList;
        }

        public void AddMinoReserve()
        {
            _list.Add(new MinoReserve());
        }
    }
}
