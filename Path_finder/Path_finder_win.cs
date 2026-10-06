using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Path_finder
{
    public partial class Path_finder_win : Form
    {
        public Path_finder_win()
        {
            InitializeComponent();
            InitializeGridBitmap();
        }

        bool drw, aim_mv;
        int beginX, beginY;


        private Point aim_mouseOffset;
        private Bitmap savedMapImage; // Резервная копия для кнопки "Сохранить"
        private Bitmap gridBitmap;

        public static int lastValidAimX = 395;
        public static int lastValidAimY = 325;

        public int[,] Ortogonal_map;
        public int[,] Radial_map;

        private void Form1_Load(object sender, EventArgs e)
        {
            Ortogonal_grid_ChBx.Enabled = false;
            Radial_grid_ChBx.Enabled = false;

            Aim_x.Text = lastValidAimX.ToString();
            Aim_y.Text = lastValidAimY.ToString();

            Map_status.Text = "Задайте запретные области";
            Map_status.ForeColor = Color.Red;

            Ortogonal_map_ChBx.Enabled = false;
            Radial_map_ChBx.Enabled = false;

            PathCount.Enabled = false;
        }

        private void Map_win_MouseDown(object sender, MouseEventArgs e)
        {
            drw = true;
            beginX = e.X;
            beginY = e.Y;
        }

        private void Map_win_MouseUp(object sender, MouseEventArgs e)
        {
            drw = false;
        }

        private void Map_win_MouseMove(object sender, MouseEventArgs e)
        {
            if (drw)
            {
                // Используем Graphics, но цвета и размеры берем из настроек
                using (Graphics g = this.Map_win.CreateGraphics())
                using (Pen p = new Pen(MapSettings.DrawObstacleColor, MapSettings.CellSize - 5)) // -5 чтобы линии были чуть тоньше ячейки
                {
                    Point point1 = new Point(beginX, beginY);
                    Point point2 = new Point(e.X, e.Y);

                    g.DrawLine(p, point1, point2);

                    beginX = e.X;
                    beginY = e.Y;
                }
            }
        }

        private void Aim_point_MouseDown(object sender, MouseEventArgs e)
        {
            aim_mv = true;
            aim_mouseOffset = new Point(e.X, e.Y);
        }

        private void Aim_point_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (aim_mv)
            {

                // Вычисляем новые координаты. 
                // e.X и e.Y - это позиция мыши ОТНОСИТЕЛЬНО самого элемента.
                int newX = this.Aim_point.Left + (e.X - aim_mouseOffset.X);
                int newY = this.Aim_point.Top + (e.Y - aim_mouseOffset.Y);

                // ====== ОГРАНИЧЕНИЕ В ПРЕДЕЛАХ MAP_WIN ======
                int maxX = this.Map_win.ClientSize.Width - this.Aim_point.Width;
                int maxY = this.Map_win.ClientSize.Height - this.Aim_point.Height;

                // Жёстко ограничиваем координаты диапазоном [0, max]
                newX = Math.Max(0, Math.Min(newX, maxX));
                newY = Math.Max(0, Math.Min(newY, maxY));
                // ============================================
                // =======================================================


                // Применяем новые координаты
                this.Aim_point.Location = new Point(newX, newY);

                // Вызываем событие отслеживания позиции
                if (!Aim_x.Focused)
                {
                    Aim_x.Text = newX.ToString();
                    lastValidAimX = newX;
                }
                if (!Aim_y.Focused)
                {
                    Aim_y.Text = newY.ToString();
                    lastValidAimY = newY;
                }
            }
        }

        private void Aim_point_MouseUp(object sender, MouseEventArgs e)
        {
            aim_mv = false;
        }

        private void Clear_map_bt_Click(object sender, EventArgs e)
        {
            drw = false;

            Map_win.BackgroundImage = gridBitmap;
            Map_win.BackgroundImageLayout = ImageLayout.None;

            // Перерисовываем сетки на gridBitmap, если чекбоксы вдруг были активны 
            // (хотя мы их ниже сбрасываем, это гарантирует чистоту)
            GridRenderer.RenderGridsToBitmap(gridBitmap, Map_win.BackColor, false, false);
            Map_win.Invalidate();

            Ortogonal_map = null;
            Radial_map = null;

            Map_status.Text = "Задайте запретные области";
            Map_status.ForeColor = Color.Red;


            Ortogonal_grid_ChBx.Enabled = false;
            Radial_grid_ChBx.Enabled = false;
            Ortogonal_grid_ChBx.Checked = false;
            Radial_grid_ChBx.Checked = false;

            Ortogonal_map_ChBx.Enabled = false;
            Radial_map_ChBx.Enabled = false;
            Ortogonal_map_ChBx.Checked = false;
            Radial_map_ChBx.Checked = false;
        }

        private void Save_map_bt_Click(object sender, EventArgs e)
        {
            savedMapImage?.Dispose();

            Rectangle screenRect = Map_win.RectangleToScreen(Map_win.ClientRectangle);

            bool wasVisible = Aim_point.Visible;
            Aim_point.Visible = false;
            Application.DoEvents();

            savedMapImage = new Bitmap(screenRect.Width, screenRect.Height);
            using (Graphics g = Graphics.FromImage(savedMapImage))
            {
                g.CopyFromScreen(screenRect.Location, Point.Empty, screenRect.Size);
            }
            Aim_point.Visible = wasVisible;

            // Делаем сохраненную карту постоянным фоном
            Map_win.BackgroundImage = savedMapImage;
            Map_win.BackgroundImageLayout = ImageLayout.None;

            Ortogonal_map = MapBuilder.Build_ortogonal_map(savedMapImage);
            Radial_map = MapBuilder.Build_radial_map(savedMapImage);

            // Map_status.Text = $"Карта сохранена. Орто: {Ortogonal_map.GetLength(0)}x{Ortogonal_map.GetLength(1)}, Радиал: {Radial_map.GetLength(0)}x{Radial_map.GetLength(1)}";

            // ===== НОВОЕ: РАЗБЛОКИРУЕМ ЧЕКБОКСЫ =====
            Ortogonal_grid_ChBx.Enabled = true;
            Radial_grid_ChBx.Enabled = true;

            // Снимаем галочки на всякий случай и просим перерисовать поле
            Ortogonal_grid_ChBx.Checked = false;
            Radial_grid_ChBx.Checked = false;
            Map_win.Invalidate();

            Ortogonal_map_ChBx.Enabled = true;
            Radial_map_ChBx.Enabled = true;
            Ortogonal_map_ChBx.Checked = false;
            Radial_map_ChBx.Checked = false;

            // Меняем текст метки
            Map_status.Text = "Карта сохранена";
            Map_status.ForeColor = Color.Green;
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            savedMapImage?.Dispose();
            base.OnFormClosed(e);
        }

        private void InitializeGridBitmap()
        {
            gridBitmap = new Bitmap(MapSettings.MapWidth, MapSettings.MapHeight);

            // Используем новый рендер для создания чистого фона
            GridRenderer.RenderGridsToBitmap(gridBitmap, Map_win.BackColor, false, false);

            Map_win.BackgroundImage = gridBitmap;
            Map_win.BackgroundImageLayout = ImageLayout.None;
        }

        private void Map_win_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем сетки поверх BackgroundImage ТОЛЬКО если карта сохранена
            if (Ortogonal_grid_ChBx.Enabled)
            {
                // Рисуем сетки
                if (Ortogonal_grid_ChBx.Checked)
                {
                    GridRenderer.DrawOrthogonal(e.Graphics, Map_win.Width, Map_win.Height);
                }

                if (Radial_grid_ChBx.Checked)
                {
                    GridRenderer.DrawRadial(e.Graphics, Map_win.Width, Map_win.Height);
                }

                // Рисуем кружки препятствий ИЗ МАССИВОВ
                if (Ortogonal_map_ChBx.Checked && Ortogonal_map != null)
                {
                    GridRenderer.DrawObstacleCirclesOrthogonal(e.Graphics, Ortogonal_map, Map_win.Width, Map_win.Height);
                }

                if (Radial_map_ChBx.Checked && Radial_map != null)
                {
                    GridRenderer.DrawObstacleCirclesRadial(e.Graphics, Radial_map, Map_win.Width, Map_win.Height);
                }
            }
        }

        private void Orthogonal_grid_ChBx_CheckedChanged(object sender, EventArgs e)
        {
            // Просто вызываем перерисовку контрола. 
            // Событие Paint само наложит сетку поверх сохраненной карты.
            Map_win.Invalidate();
        }

        private void Radial_grid_ChBx_CheckedChanged(object sender, EventArgs e)
        {
            // Просто вызываем перерисовку контрола. 
            // Событие Paint само наложит сетку поверх сохраненной карты.
            Map_win.Invalidate();
        }

        private void Orthogonal_map_ChBx_CheckedChanged(object sender, EventArgs e)
        {
            Map_win.Invalidate();
        }

        private void Radial_map_ChBx_CheckedChanged(object sender, EventArgs e)
        {
            Map_win.Invalidate();
        }

        /// <summary>
        /// Проверяет валидность значения в TextBox и подсвечивает красным при ошибке
        /// </summary>
        private void ValidateTextBox(TextBox textBox)
        {
            int max = textBox == Aim_x
                ? Map_win.ClientSize.Width - Aim_point.Width
                : Map_win.ClientSize.Height - Aim_point.Height;

            bool isValid = AimCounter.ValidateTextBox(textBox, 0, max);
            AimCounter.UpdateTextBoxStyle(textBox, isValid);
        }

        /// <summary>
        /// Пытается применить значение из TextBox. При ошибке возвращает старое значение.
        /// </summary>
        private void ApplyTextBoxValue(TextBox textBox, bool isX)
        {
            int max = isX
                ? Map_win.ClientSize.Width - Aim_point.Width
                : Map_win.ClientSize.Height - Aim_point.Height;

            int defaultValue = isX ? lastValidAimX : lastValidAimY;

            int newValue = AimCounter.ApplyTextBoxValue(textBox, 0, max, defaultValue);

            // Применяем новую координату
            if (isX)
            {
                Aim_point.Location = new Point(newValue, Aim_point.Top);
                lastValidAimX = newValue;
            }
            else
            {
                Aim_point.Location = new Point(Aim_point.Left, newValue);
                lastValidAimY = newValue;
            }

            AimCounter.UpdateTextBoxStyle(textBox, true);
        }

        private void Aim_x_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(Aim_x);
        }

        private void Aim_x_Leave(object sender, EventArgs e)
        {
            ApplyTextBoxValue(Aim_x, isX: true);
        }

        private void Aim_x_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyTextBoxValue(Aim_x, true);
                e.Handled = true;
                e.SuppressKeyPress = true; // Убирает системный звук "бип" при нажатии Enter
            }
        }

        private void Aim_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Запрещаем ввод всего, кроме цифр и управляющих символов (Backspace, Enter)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Aim_y_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(Aim_y);
        }

        private void Aim_y_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyTextBoxValue(Aim_y, false);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Aim_y_Leave(object sender, EventArgs e)
        {
            ApplyTextBoxValue(Aim_y, false);
        }

        private void Aim_y_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PathCount_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            PathCount.Enabled = false;
            if (PathCount_mode.SelectedIndex == 0)
            {
                Path_calculation.Wave_algorithm(Ortogonal_map);
            }
            if (PathCount_mode.SelectedIndex == 1)
            {
                Path_calculation.Wave_algorithm(Radial_map);
            }
            Cursor = Cursors.Default;
            PathCount.Enabled = true;

        }

        private void PathCount_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PathCount.Enabled = true;
        }
    }
}

