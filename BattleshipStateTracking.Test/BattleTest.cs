using System;
using System.Linq;
using BattleshipStateTracking.Models;
using BattleshipStateTracking.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleshipStateTracking.Test
{
    [TestClass]
    public class BattleTest
    {
        [TestMethod]
        public void TestPositionEquality()
        {
            var pos1 = new Position(8, 5);
            var pos2 = new Position(8, 5);
            var pos3 = new Position(5, 8);
            Assert.IsTrue(pos1.Equals(pos2));
            Assert.IsFalse(pos1.Equals(pos3));
        }

        [TestMethod]
        public void TestCreateBoard_Success()
        {
            var service = new BattlefieldService();

            service.CreateBoard(100, 100);

            Assert.AreEqual(service.Battlefield.Row, 100);
            Assert.AreEqual(service.Battlefield.Column, 100);
        }

        [TestMethod]
        public void TestCreateBoard_Fail()
        {
            var service = new BattlefieldService();

            Assert.ThrowsException<InvalidOperationException>(() => service.CreateBoard(-1));

            service.CreateBoard();

            Assert.ThrowsException<InvalidOperationException>(() => service.CreateBoard());
        }

        [TestMethod]
        public void TestAddBattleship_Success()
        {
            var service = new BattlefieldService();
            service.CreateBoard();
            service.AddBattleship(0, 0, EnumOrientation.Horizontal, 3);
            var ship = service.Battlefield.Battleships[0, 0];
            Assert.AreEqual(service.Battlefield.Battleships[0, 0], ship);
            Assert.AreEqual(service.Battlefield.Battleships[0, 1], ship);
            Assert.AreEqual(service.Battlefield.Battleships[0, 2], ship);
            Assert.AreEqual(service.Battlefield.Battleships[0, 3], null);
            Assert.IsTrue(ship.Positions.All(p => p.X == 0));
            Assert.IsTrue(ship.Positions.Count(p => p.Y == 0) == 1);
            Assert.IsTrue(ship.Positions.Count(p => p.Y == 1) == 1);
            Assert.IsTrue(ship.Positions.Count(p => p.Y == 2) == 1);
            Assert.IsTrue(ship.Positions.Count == 3);
        }

        [TestMethod]
        public void TestAddBattleship_Fail()
        {
            var service = new BattlefieldService();
            service.CreateBoard();
            Assert.ThrowsException<InvalidOperationException>(() =>
                service.AddBattleship(8, 0, EnumOrientation.Vertical, 3));
            Assert.ThrowsException<ArgumentException>(() => service.AddBattleship(8, 0, EnumOrientation.Horizontal, 0));
            service.AddBattleship(8, 0, EnumOrientation.Horizontal, 3);
            Assert.ThrowsException<InvalidOperationException>(() =>
                service.AddBattleship(8, 0, EnumOrientation.Vertical, 2));
            Assert.ThrowsException<InvalidOperationException>(() =>
                service.AddBattleship(7, 2, EnumOrientation.Vertical, 2));
        }

        [TestMethod]
        public void TestAttack()
        {
            var service = new BattlefieldService();
            service.CreateBoard();
            service.AddBattleship(0, 0, EnumOrientation.Horizontal, 2);
            Assert.ThrowsException<InvalidOperationException>(() => service.Attack(-1, 0));
            Assert.ThrowsException<InvalidOperationException>(() => service.Attack(0, 10));
            Assert.AreEqual(EnumAttackResult.Miss.ToString(), service.Attack(1,0));
            Assert.AreEqual(EnumAttackResult.Miss.ToString(), service.Attack(1,2));
            Assert.AreEqual(EnumAttackResult.Hit.ToString(), service.Attack(0,0));
            Assert.AreEqual(EnumAttackResult.Hit.ToString(), service.Attack(0,1));
            Assert.AreEqual(0, service.Battlefield.Battleships[0,0].Positions.Count);

        }
    }
}
