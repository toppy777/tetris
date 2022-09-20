using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.Minos
{
    public class MinoBindFactory : IMinoBindFactory
    {
        MinoPieceView _minoPieceViewPrefab;

        public MinoBindFactory(
            Prefabs prefabs
        ) {
            _minoPieceViewPrefab = prefabs.MinoPieceViewPrefab;
        }

        public IMinoBind CreateMinoBind(Mino mino)
        {
            return new MinoBind(mino, _minoPieceViewPrefab);
        }
    }
}
