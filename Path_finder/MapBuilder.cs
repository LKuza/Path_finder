using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_finder
{
    public static class MapBuilder
    {
        // Размеры панели
        private const int MapWidth = 800;
        private const int MapHeight = 660;
        private const int CellSize = 15;

        // Параметры радиальной сетки
        private static readonly Point RadialCenter = new Point(MapWidth / 2, MapHeight); // (400, 660)
        private const int MaxRadius = 772; // Примерно sqrt(400^2 + 660^2)
        private const int RingCount = 52;  // 772 / 15
        private const int SectorCount = 36; // 180 градусов / 5

        /// <summary>
        /// Строит ортогональную карту. 
        /// 0 = препятствие (закрашено > 50%), 1 = свободно
        /// </summary>
        public static int[,] Build_ortogonal_map(Bitmap image)
        {
            int cols = (int)Math.Ceiling((double)MapWidth / CellSize); // 54
            int rows = (int)Math.Ceiling((double)MapHeight / CellSize); // 44
            int[,] map = new int[cols, rows];

            for (int c = 0; c < cols; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    int drawnPixels = 0;
                    int totalPixels = 0;

                    int startX = c * CellSize;
                    int startY = r * CellSize;
                    int endX = Math.Min(startX + CellSize, MapWidth);
                    int endY = Math.Min(startY + CellSize, MapHeight);

                    for (int x = startX; x < endX; x++)
                    {
                        for (int y = startY; y < endY; y++)
                        {
                            totalPixels++;
                            Color pixel = image.GetPixel(x, y);

                            // Эвристика: пользователь рисует полупрозрачным красным (255, 0, 0).
                            // Проверяем, что красный канал значительно превышает зеленый и синий.
                            // Это игнорирует серые/синие линии сетки и светлый фон.
                            if (pixel.R > pixel.G + 40 && pixel.R > pixel.B + 40)
                            {
                                drawnPixels++;
                            }
                        }
                    }

                    // Если закрашено больше 10% ячейки, считаем её препятствием (0)
                    map[c, r] = ((double)drawnPixels / totalPixels > 0.1) ? 0 : 1;
                }
            }
            return map;
        }

        /// <summary>
        /// Строит радиальную карту.
        /// 0 = препятствие (закрашено > 10%), 1 = свободно
        /// </summary>
        public static int[,] Build_radial_map(Bitmap image)
        {
            int[,] drawnCounts = new int[RingCount, SectorCount];
            int[,] totalCounts = new int[RingCount, SectorCount];

            // Проходим по всем пикселям изображения один раз (это очень быстро)
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int dx = x - RadialCenter.X;
                    int dy = RadialCenter.Y - y; // dy положительный вверх

                    double distance = Math.Sqrt(dx * dx + dy * dy);
                    int ring = (int)(distance / CellSize);

                    if (ring < RingCount)
                    {
                        // Atan2 возвращает угол от 0 (право) до PI (лево) для верхней половины
                        double angleRad = Math.Atan2(dy, dx);
                        double angleDeg = angleRad * 180.0 / Math.PI;

                        // Преобразуем: Право=360(0), Верх=270, Лево=180
                        double targetAngle = 360.0 - angleDeg;

                        int sector = (int)((targetAngle - 180.0) / 5.0);

                        if (sector >= 0 && sector < SectorCount)
                        {
                            totalCounts[ring, sector]++;

                            Color pixel = image.GetPixel(x, y);
                            if (pixel.R > pixel.G + 40 && pixel.R > pixel.B + 40)
                            {
                                drawnCounts[ring, sector]++;
                            }
                        }
                    }
                }
            }

            // Формируем итоговую карту
            int[,] map = new int[RingCount, SectorCount];
            for (int r = 0; r < RingCount; r++)
            {
                for (int s = 0; s < SectorCount; s++)
                {
                    if (totalCounts[r, s] > 0)
                    {
                        map[r, s] = ((double)drawnCounts[r, s] / totalCounts[r, s] > 0.2) ? 0 : 1;
                    }
                    else
                    {
                        map[r, s] = 1; // Пустые области считаем свободными
                    }
                }
            }
            return map;
        }
    }
}
