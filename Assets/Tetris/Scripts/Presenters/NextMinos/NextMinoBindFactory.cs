using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Presenters.MinoPieces;
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

        public INextMinoBind CreateNextMinoBind(MinoReserveList minoReserveList)
        {
            return new NextMinoBind(minoReserveList, _minoPieceViewPrefab);
        }
    }
}
