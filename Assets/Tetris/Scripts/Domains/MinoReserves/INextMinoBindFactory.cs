namespace Tetris.Scripts.Domains.MinoReserves
{
    public interface INextMinoBindFactory
    {
        INextMinoBind CreateNextMinoBind(MinoReserveList minoReserveList);
    }
}
