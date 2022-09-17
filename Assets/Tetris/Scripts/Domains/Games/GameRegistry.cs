using UnityEngine;

namespace Tetris.Scripts.Domains.Games
{
    public class GameRegistry
    {
        public Game CurrentGame { get; private set; }
        public void Register(Game game) => CurrentGame = game;
    }
}
