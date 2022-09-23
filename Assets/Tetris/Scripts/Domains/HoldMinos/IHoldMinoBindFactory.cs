namespace Tetris.Scripts.Domains.HoldMinos
{
    public interface IHoldMinoBindFactory
    {
        IHoldMinoBind CreateHoldMinoBind(HoldMino holdMino);
    }
}
