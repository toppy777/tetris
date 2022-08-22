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
    }
}
