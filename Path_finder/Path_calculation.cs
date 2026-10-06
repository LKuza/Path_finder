using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path_finder
{
    public static class Path_calculation
    {
        public static int aimX = Path_finder_win.lastValidAimX;
        public static int aimY = Path_finder_win.lastValidAimY;

        //На вход получаем массив карты ортоганальной/радиальной
        public static async Task Wave_algorithm(int[,] Map)
        {
            int i, j;//переменные для движение по массиву
                     // Сюда пишем код для алгоритма 
            await Task.Delay(300000);// Задержка для проверки работы кнопок(почему то не работает)
        }

        // можно добавить ещё один алгоритм, если хотим
    }
}
