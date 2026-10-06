using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_finder
{
    public static class GridRenderer
    {
        /// <summary>
        /// Рисует ортогональную сетку на переданном объекте Graphics.
        /// </summary>
        public static void DrawOrthogonal(Graphics g, int width, int height)
        {
            using (Pen pen = new Pen(MapSettings.GridOrthogonalColor, 1))
            {
                // Вертикальные линии
                for (int x = 0; x <= width; x += MapSettings.CellSize)
                {
                    g.DrawLine(pen, x, 0, x, height);
                }

                // Горизонтальные линии
                for (int y = 0; y <= height; y += MapSettings.CellSize)
                {
                    g.DrawLine(pen, 0, y, width, y);
                }
            }
        }

        /// <summary>
        /// Рисует радиальную сетку на переданном объекте Graphics.
        /// </summary>
        public static void DrawRadial(Graphics g, int width, int height)
        {
            using (Pen pen = new Pen(MapSettings.GridRadialColor, 1))
            {
                Point center = new Point(width / 2, height);
                int maxRadius = (int)Math.Sqrt(Math.Pow(center.X, 2) + Math.Pow(center.Y, 2));

                // Полуокружности
                for (int r = MapSettings.CellSize; r <= maxRadius; r += MapSettings.CellSize)
                {
                    g.DrawArc(pen, center.X - r, center.Y - r, r * 2, r * 2, 180, 180);
                }

                // Радиальные линии (шаг 5 градусов)
                for (int angle = 180; angle <= 360; angle += 5)
                {
                    double radians = angle * Math.PI / 180.0;
                    int endX = center.X + (int)(maxRadius * Math.Cos(radians));
                    int endY = center.Y + (int)(maxRadius * Math.Sin(radians));

                    g.DrawLine(pen, center.X, center.Y, endX, endY);
                }
            }
        }

        /// <summary>
        /// Полностью перерисовывает Bitmap с сетками (используется при очистке или инициализации).
        /// </summary>
        public static void RenderGridsToBitmap(Bitmap bitmap, Color backColor, bool drawOrtho, bool drawRadial)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(backColor);

                if (drawOrtho)
                {
                    DrawOrthogonal(g, bitmap.Width, bitmap.Height);
                }

                if (drawRadial)
                {
                    DrawRadial(g, bitmap.Width, bitmap.Height);
                }
            }
        }

        /// <summary>
        /// Рисует кружки в ячейках с препятствиями для ортогональной сетки
        /// Использует массив map, где 0 = препятствие, 1 = свободно
        /// </summary>
        public static void DrawObstacleCirclesOrthogonal(Graphics g, int[,] map, int width, int height)
        {
            if (map == null) return;

            int cols = map.GetLength(0);
            int rows = map.GetLength(1);

            // Ярко-красный для видимости
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(200, 255, 0, 0)))
            {
                for (int col = 0; col < cols; col++)
                {
                    for (int row = 0; row < rows; row++)
                    {
                        // Проверяем значение из массива
                        if (map[col, row] == 0) // 0 = препятствие
                        {
                            // Центр ячейки
                            float centerX = col * MapSettings.CellSize + MapSettings.CellSize / 2.0f;
                            float centerY = row * MapSettings.CellSize + MapSettings.CellSize / 2.0f;

                            // Кружок диаметром 15
                            float diameter = MapSettings.CellSize;
                            float x = centerX - diameter / 2.0f;
                            float y = centerY - diameter / 2.0f;

                            g.FillEllipse(brush, x, y, diameter, diameter);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Рисует кружки в ячейках с препятствиями для радиальной сетки
        /// Использует массив map, где 0 = препятствие, 1 = свободно
        /// </summary>
        public static void DrawObstacleCirclesRadial(Graphics g, int[,] map, int width, int height)
        {
            if (map == null) return;

            int rings = map.GetLength(0);
            int sectors = map.GetLength(1);

            Point center = new Point(width / 2, height);

            // Ярко-синий для видимости
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(200, 0, 100, 255)))
            {
                for (int ring = 0; ring < rings; ring++)
                {
                    for (int sector = 0; sector < sectors; sector++)
                    {
                        // Проверяем значение из массива
                        if (map[ring, sector] == 0) // 0 = препятствие
                        {
                            // Центр ячейки в полярных координатах
                            float radius = (ring + 0.5f) * MapSettings.CellSize;
                            float angleDeg = 180.0f + (sector + 0.5f) * 5.0f;
                            float angleRad = angleDeg * (float)Math.PI / 180.0f;

                            // Преобразуем в декартовы координаты
                            float centerX = center.X + radius * (float)Math.Cos(angleRad);
                            float centerY = center.Y + radius * (float)Math.Sin(angleRad);

                            // Кружок диаметром 15
                            float diameter = MapSettings.CellSize;
                            float x = centerX - diameter / 2.0f;
                            float y = centerY - diameter / 2.0f;

                            g.FillEllipse(brush, x, y, diameter, diameter);
                        }
                    }
                }
            }
        }

    }
}
