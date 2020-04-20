using BattleshipStateTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracking.Services
{
    public class BattlefieldService : IBattlefieldService
    {
        public Battlefield Battlefield;

        public void CreateBoard(int length = 10, int width = 10)
        {
            if (Battlefield != null) throw new InvalidOperationException("Board already created.");
            
            if (length <= 0 || width <=0) throw new InvalidOperationException("Board length and width must be greater than 0.");

            Battlefield = new Battlefield(length, width);
        }

        public void AddBattleship(int startX, int startY, EnumOrientation orientation, int length)
        {
            if (Battlefield == null)
                throw new InvalidOperationException("Create a board first.");

            if (length == 0) throw new ArgumentException("Battleship length must be greater than 0");

            var battleshipPositions = GetPositions(startX, startY, orientation, length).ToArray();

            var isValid = ValidatePositions(battleshipPositions);

            if (!isValid)
                throw new InvalidOperationException(
                    "Battleship cannot fit entirely on the board or has conflicts with existing ships.");

            var battleship = new Battleship(battleshipPositions);

            foreach (var battleshipPosition in battleshipPositions)
            {
                Battlefield.Battleships[battleshipPosition.X, battleshipPosition.Y] = battleship;
            }
        }

        public string Attack(int x, int y)
        {
            if (Battlefield == null)
                throw new InvalidOperationException("Create a board first.");

            var position = new Position(x, y);

            if (!IsPositionOnField(position))
                throw new InvalidOperationException("Attacked outside of the board");

            if (Battlefield.Battleships[x, y] == null) return EnumAttackResult.Miss.ToString();
            
            var battleship = Battlefield.Battleships[x, y];

            battleship.Positions.Remove(position);

            return EnumAttackResult.Hit.ToString();
        }

        private IEnumerable<Position> GetPositions(int startX, int startY, EnumOrientation orientation, int length)
        {
            var positions = new Position[length];

            for (var i = 0; i < length; i++)
            {
                if (orientation == EnumOrientation.Horizontal)
                    positions[i] = new Position(startX, startY + i);
                else
                    positions[i] = new Position(startX + i, startY);
            }

            return positions;
        }

        private bool ValidatePositions(IEnumerable<Position> positions)
        {
            foreach (var position in positions)
            {
                if (!IsPositionOnField(position)) return false;

                if (Battlefield.Battleships[position.X, position.Y] != null) return false;
            }

            return true;
        }

        private bool IsPositionOnField(Position position)
        {
            if (position.X < 0) return false;

            if (position.X >= Battlefield.Row) return false;

            if (position.Y < 0) return false;

            if (position.Y >= Battlefield.Column) return false;

            return true;
        }
    }
}
