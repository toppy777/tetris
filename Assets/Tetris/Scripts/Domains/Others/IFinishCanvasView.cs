using UnityEngine.Events;

namespace Tetris.Scripts.Domains.Others
{
    public interface IFinishCanvasView
    {
        void Display();
        void UnDisplay();
        void SetScore(int score);
        void DisplayScore();
        void DisplayFinishText();
        void SetRestartButtonClick(UnityAction action);
        void SetBackToTitleButton(UnityAction action);
        void Destroy();
    }
}
