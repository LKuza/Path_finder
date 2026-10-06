using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_finder
{
    // Статический класс для хранения всех настроек карты в одном месте
    public static class MapSettings
    {
        public const int MapWidth = 800;
        public const int MapHeight = 660;
        public const int CellSize = 15;

        // Цвета тоже можно вынести сюда, чтобы легко менять тему
        public static readonly Color GridOrthogonalColor = Color.Gray;
        public static readonly Color GridRadialColor = Color.Blue;
        public static readonly Color DrawObstacleColor = Color.FromArgb(128, 255, 0, 0);
    }
}
