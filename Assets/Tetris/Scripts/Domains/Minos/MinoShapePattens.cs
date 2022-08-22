using UnityEngine;
using System.Collections.Generic;

namespace Tetris.Scripts.Domains.Minos
{
    public class MinoShapePattens
    {
        List<List<Vector2Int>> _list;
        int _index;

        public MinoShapePattens(List<List<Vector2Int>> pattens)
        {
            _list = pattens;
            _index = 0;
        }

        public List<Vector2Int> GetShape()
        {
            return _list[_index];
        }

        public void Next()
        {
            _index++;
            if (_index == _list.Count) {
                _index = 0;
            }
        }

        public void Prev()
        {
            _index--;
            if (_index < 0) {
                _index = _list.Count-1;
            }
        }
    }
}
