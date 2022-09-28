namespace Tetris.Scripts.Domains.Scores
{
    public interface IScoreView
    {
        IScoreDataView GetScoreDataView();
        void Destroy();
    }
}
