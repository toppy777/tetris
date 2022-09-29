using UnityEngine;
using System.Collections.Generic;
using UniRx;
using Tetris.Scripts.Domains.HoldMinos;
using Tetris.Scripts.Domains.MinoShapes;
using Tetris.Scripts.Domains.MinoColors;
using Tetris.Scripts.Presenters.MinoPieces;
using Tetris.Scripts.Presenters.Boards;

namespace Tetris.Scripts.Presenters.HoldMinos
{
    public class HoldMinoBind : IHoldMinoBind
    {
        Subject<Unit> _whenDeleteView = new();
        private readonly CompositeDisposable _disposable = new();

        public HoldMinoBind(
            HoldMino holdMino,
            MinoPieceView minoPieceViewPrefab
        )
        {
            Vector2 pos = new Vector2(-1.3f, 3.6f);

            MinoShape minoShapePatten = new MinoShape(holdMino.GetMinoType());
            MinoColor minoColor = new MinoColor(holdMino.GetMinoType());

            // minoのview作成
            List<MinoPieceView> pieceViews = new List<MinoPieceView>();
            int pieceNum = 4;
            for (int i = 0; i < pieceNum; i++) {
                MinoPieceView copy = GameObject.Instantiate(minoPieceViewPrefab);
                copy.SetColor(minoColor.Value);
                float squareSize = BoardData.squareSize;
                copy.SetPosition(new Vector2(pos.x + minoShapePatten.GetShape()[i].x * squareSize, pos.y + minoShapePatten.GetShape()[i].y * squareSize));
                pieceViews.Add(copy);
            }

            foreach (MinoPieceView view in pieceViews) {
                holdMino.WhenSet.Subscribe(_ => {
                    if (view != null) {
                        GameObject.Destroy(view.gameObject);
                    }
                }).AddTo(_disposable);

                _whenDeleteView.Subscribe(_ => {
                    if (view != null) {
                        GameObject.Destroy(view.gameObject);
                    }
                }).AddTo(_disposable);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void DestroyView()
        {
            _whenDeleteView.OnNext(Unit.Default);
        }
    }
}
