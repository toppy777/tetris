using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Presenters.ScriptableObjects;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.MinoShadows
{
    public class MinoShadowBindFactory : IMinoShadowBindFactory
    {
        MinoPieceView _minoPieceViewPrefab;

        public MinoShadowBindFactory(
            Prefabs prefabs
        )
        {
            _minoPieceViewPrefab = prefabs.MinoPieceViewPrefab;
        }

        public void CreateMinoShadowBind(MinoShadow minoShadow)
        {
            new MinoShadowBind(minoShadow, _minoPieceViewPrefab);
        }
    }
}
