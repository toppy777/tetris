using UnityEngine;
using Tetris.Scripts.Application.Games;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Presenters.ScriptableObjects;

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

        public void CreateMinoBind(Mino mino)
        {
            new MinoBind(mino, _minoPieceViewPrefab);
        }
    }
}
