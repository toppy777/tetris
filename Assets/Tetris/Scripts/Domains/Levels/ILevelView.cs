namespace Tetris.Scripts.Domains.Levels
{
    public interface ILevelView
    {
        ILevelDataView GetScoreDataView();
        void Destroy();
    }
}
