using System;

namespace BattleshipStateTracking.Models
{
    public class Position
    {
        public int X { get; }

        public int Y { get; }


        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return obj is Position position && (position.X == X && position.Y == Y);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }
}
