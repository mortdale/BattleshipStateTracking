namespace BattleshipStateTracking.Models
{
    public class Battlefield
    {
        public int Row { get; }
        public int Column { get; }

        public Battleship[,] Battleships { get; private set; }

        public Battlefield(int row = 10, int column = 10)
        {
            Row = row;
            Column = column;
            Battleships = new Battleship[row, column];
        }
    }
}
