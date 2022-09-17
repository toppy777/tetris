namespace Tetris.Scripts.Domains.RowCounts
{
    public class RowCount
    {
        int _value = 0;
        public int Value => _value;

        public void Add(int rowCount)
        {
            _value += rowCount;
        }
    }
}
