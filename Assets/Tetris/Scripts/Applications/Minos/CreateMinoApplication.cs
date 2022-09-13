using Tetris.Scripts.Domains.Minos;
using Tetris.Scripts.Domains.MinoReserves;

namespace Tetris.Scripts.Application.Minos
{
    public class CreateMinoApplication
    {
        Mino _mino;
        MinoFactory _minoFactory;
        MinoReserveList _minoReserveList;

        public CreateMinoApplication(
            Mino mino,
            MinoFactory minoFactory,
            MinoReserveList minoReserveList
        ) {
            _mino = mino;
            _minoFactory = minoFactory;
            _minoReserveList = minoReserveList;
        }

        public void Execute()
        {
            _mino = _minoFactory.CreateMino(_minoReserveList.PopMinoType());
        }
    }
}
