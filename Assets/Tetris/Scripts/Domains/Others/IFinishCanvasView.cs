using UnityEngine.Events;

namespace Tetris.Scripts.Domains.Others
{
    public interface IFinishCanvasView
    {
        void Display();
        void UnDisplay();
        void SetRestartButtonClick(UnityAction action);
        void SetBackToTitleButton(UnityAction action);
    }
}
