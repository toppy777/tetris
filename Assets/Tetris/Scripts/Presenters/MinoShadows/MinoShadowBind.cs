using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Presenters.MinoPieces;
using Tetris.Scripts.Presenters.Boards;

namespace Tetris.Scripts.Presenters.MinoShadows
{
    public class MinoShadowBind : IMinoShadowBind, IDisposable
    {
        Subject<Unit> _whenDeleteView = new();
        private readonly CompositeDisposable _disposable = new();

        public MinoShadowBind(
            MinoShadow minoShadow,
            MinoPieceView minoPieceViewPrefab
        )
        {
            int pieceNum = 4;
            Color32 color = new Color32(255, 255, 255, 70);

            List<MinoPieceView> shadowPieceViews = new List<MinoPieceView>();
            for (int i = 0; i < pieceNum; i++) {
                var copy = GameObject.Instantiate(minoPieceViewPrefab);
                copy.GetComponent<Renderer>().material.color = color;
                shadowPieceViews.Add(copy);
            }

            minoShadow.WhenPositionsChange.Subscribe(positions => {
                for (int i = 0; i < shadowPieceViews.Count; i++) {
                    shadowPieceViews[i].transform.position = GetPosition(positions[i]);
                }
            }).AddTo(_disposable);

            _whenDeleteView.Subscribe(_ => {
                foreach (MinoPieceView view in shadowPieceViews) {
                    if (view != null) {
                        GameObject.Destroy(view.gameObject);
                    }
                }
            }).AddTo(_disposable);
        }

        public Vector2 GetPosition(Vector2Int indexPos)
        {
            float x = indexPos.x * BoardData.squareSize;
            float y = indexPos.y * BoardData.squareSize;
            return new Vector2(x + BoardData.beginX, y + BoardData.beginY);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void DestroyView()
        {
            _whenDeleteView.OnNext(Unit.Default);
        }
    }

}
