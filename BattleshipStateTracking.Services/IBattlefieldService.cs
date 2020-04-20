using BattleshipStateTracking.Models;

namespace BattleshipStateTracking.Services
{
    public interface IBattlefieldService
    {
        void CreateBoard(int length = 10, int width = 10);
        void AddBattleship(int startX, int startY, EnumOrientation orientation, int length);
        string Attack(int x, int y);
        
    }
}