using UniRx;

namespace Tetris.Scripts.Domains.Minos
{
    public interface IMinoBindFactory
    {
        void CreateMinoBind(Mino mino, CompositeDisposable disposable);
    }
}
