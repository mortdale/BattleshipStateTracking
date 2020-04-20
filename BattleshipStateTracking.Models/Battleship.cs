using System.Collections.Generic;

namespace BattleshipStateTracking.Models
{
    public class Battleship
    {
        private readonly int _length;
        private readonly int _width;
        private readonly EnumOrientation _orientation;

        public readonly HashSet<Position> Positions;

        public Battleship(IEnumerable<Position> positions)
        {
            Positions = new HashSet<Position>(positions);
        }

        public Battleship(int startX, int startY, EnumOrientation orientation, int length, int width = 1)
        {
            var startPosition = new Position(startX, startY);
            _orientation = orientation;
            _length = length;
            _width = width;
            Positions = new HashSet<Position>();
        }
    }
}
