using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_finder
{
    public static class AimCounter
    {
        /// <summary>
        /// Проверяет, является ли значение в TextBox допустимым числом в заданном диапазоне
        /// </summary>
        /// <param name="textBox">TextBox для проверки</param>
        /// <param name="min">Минимальное допустимое значение</param>
        /// <param name="max">Максимальное допустимое значение</param>
        /// <returns>true если значение валидно, false если нет</returns>
        public static bool ValidateTextBox(TextBox textBox, int min, int max)
        {
            // Пустое поле считаем допустимым (пока пользователь не нажал Enter)
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                return true;
            }

            // Пытаемся преобразовать текст в число
            if (int.TryParse(textBox.Text, out int value))
            {
                // Проверяем, попадает ли число в диапазон
                return value >= min && value <= max;
            }

            // Если это не число (например, буквы)
            return false;
        }

        /// <summary>
        /// Применяет значение из TextBox к координате точки
        /// </summary>
        /// <param name="textBox">TextBox со значением</param>
        /// <param name="min">Минимальное допустимое значение</param>
        /// <param name="max">Максимальное допустимое значение</param>
        /// <param name="defaultValue">Значение, которое вернется при ошибке</param>
        /// <returns>Примененное значение (валидное или defaultValue)</returns>
        public static int ApplyTextBoxValue(TextBox textBox, int min, int max, int defaultValue)
        {
            if (int.TryParse(textBox.Text, out int value))
            {
                if (value >= min && value <= max)
                {
                    return value; // Значение валидно, возвращаем его
                }
            }

            // Значение невалидно (или это не число) — возвращаем старое безопасное значение
            // и обновляем текст в поле, чтобы пользователь видел, что ввод не прошел
            textBox.Text = defaultValue.ToString();
            return defaultValue;
        }

        /// <summary>
        /// Обновляет визуальный стиль TextBox в зависимости от валидности
        /// </summary>
        /// <param name="textBox">TextBox для обновления</param>
        /// <param name="isValid">true если значение валидно</param>
        public static void UpdateTextBoxStyle(TextBox textBox, bool isValid)
        {
            textBox.BackColor = isValid ? Color.White : Color.LightCoral;
        }
    }
}
