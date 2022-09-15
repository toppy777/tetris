using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Presenters.MinoPieces;


namespace Tetris.Scripts.Presenters.HoldMinos
{
    public class HoldMinoBindFactory : IHoldMinoBindFactory
    {
        MinoPieceView _minoPieceViewPrefab;

        public HoldMinoBindFactory(
            Prefabs prefabs
        ) {
            _minoPieceViewPrefab = prefabs.MinoPieceViewPrefab;
        }

        public void CreateHoldMinoBind(HoldMino holdMino)
        {
            new HoldMinoBind(holdMino, _minoPieceViewPrefab);
        }
    }
}
