using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;
using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Presenters.MinoPieces;

namespace Tetris.Scripts.Presenters.Minos
{
    public class MinoBind : IMinoBind, IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        public MinoBind(
            Mino mino,
            MinoPieceView minoPieceViewPrefab
        ) {
            int pieceNum = 4;

            // minoのview作成
            List<MinoPieceView> minoViews = new List<MinoPieceView>();
            for (int i = 0; i < pieceNum; i++) {
                var copy = GameObject.Instantiate(minoPieceViewPrefab);
                copy.AddTo(_disposable);
                copy.GetComponent<Renderer>().material.color = mino.Color;
                minoViews.Add(copy);
            }

            // minoのpositionが変更になった時の処理を登録
            List<IObservable<Vector2Int>> changeObservables = mino.GetWhenChangePositionObservables();
            int index = 0;
            foreach (MinoPieceView minoView in minoViews) {
                changeObservables[index++].Subscribe(pos => {
                    minoView.transform.position = GetPosition(pos);
                }).AddTo(_disposable);
            }

            // minoPieceが削除された時の処理を登録
            index = 0;
            List<IObservable<Unit>> deleteObservables = mino.GetWhenDeleteObservables();
            foreach (MinoPieceView minoView in minoViews) {
                deleteObservables[index++].Subscribe(_ => {
                    GameObject.Destroy(minoView.gameObject);
                }).AddTo(_disposable);
            }
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
