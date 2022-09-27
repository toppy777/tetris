using UniRx;

namespace Tetris.Scripts.Domains.MinoShadows
{
    public interface IMinoShadowBindFactory
    {
        void CreateMinoShadowBind(MinoShadow minoShadow, CompositeDisposable disposable);
    }
}
