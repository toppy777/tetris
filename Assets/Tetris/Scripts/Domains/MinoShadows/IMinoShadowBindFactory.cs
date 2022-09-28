using UniRx;

namespace Tetris.Scripts.Domains.MinoShadows
{
    public interface IMinoShadowBindFactory
    {
        IMinoShadowBind CreateMinoShadowBind(MinoShadow minoShadow, CompositeDisposable disposable);
    }
}
