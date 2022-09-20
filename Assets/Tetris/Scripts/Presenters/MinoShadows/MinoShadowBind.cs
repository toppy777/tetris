using UnityEngine;
using System.Collections.Generic;
using System;
using UniRx;
using System;
using Tetris.Scripts.Domains.MinoShadows;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.MinoShadows
{
    public class MinoShadowBind : IMinoShadowBind, IDisposable
    {
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
                copy.AddTo(_disposable);
                copy.GetComponent<Renderer>().material.color = color;
                shadowPieceViews.Add(copy);
            }

            minoShadow.WhenPositionsChange.Subscribe(positions => {
                for (int i = 0; i < shadowPieceViews.Count; i++) {
                    shadowPieceViews[i].transform.position = GetPosition(positions[i]);
                }
            }).AddTo(_disposable);
        }

        private const float xBegin = 0.24f;
        private const float yBegin = 0.24f;
        public Vector2 GetPosition(Vector2Int indexPos)
        {
            float x = indexPos.x * 0.16f;
            float y = indexPos.y * 0.16f;
            return new Vector2(x + xBegin, y + yBegin);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }

}
