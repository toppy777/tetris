using UnityEngine;
using Tetris.Scripts.Application.Games;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Domains.MinoReserves;

namespace Tetris.Scripts.Presenters.NextMinos
{
    public class NextMinoBindFactory : INextMinoBindFactory
    {
        MinoPieceView _minoPieceViewPrefab;

        public NextMinoBindFactory(
            Prefabs prefabs
        )
        {
            _minoPieceViewPrefab = prefabs.MinoPieceViewPrefab;
        }

        public void CreateNextMinoBind(MinoReserveList minoReserveList)
        {
            new NextMinoBind(minoReserveList, _minoPieceViewPrefab);
        }
    }
}
