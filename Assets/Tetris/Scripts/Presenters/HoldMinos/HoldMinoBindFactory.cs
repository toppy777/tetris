using UnityEngine;
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

        public IHoldMinoBind CreateHoldMinoBind(HoldMino holdMino)
        {
            Debug.Log("Create Hold Mino Bind");
            return new HoldMinoBind(holdMino, _minoPieceViewPrefab);
        }
    }
}
