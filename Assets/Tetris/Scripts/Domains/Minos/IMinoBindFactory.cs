using UniRx;

namespace Tetris.Scripts.Domains.Minos
{
    public interface IMinoBindFactory
    {
        IMinoBind CreateMinoBind(Mino mino, CompositeDisposable disposable);
    }
}
