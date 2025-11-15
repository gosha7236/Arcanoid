using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid.Clasess
{
    /// <summary>
    /// Класс "Кирпич" с количеством жизней
    /// </summary>
    public class Brick
    {
        /// <summary>
        /// хранит положение
        /// </summary>
        public Rectangle Rect;
        /// <summary>
        /// хранит ряд
        /// </summary>
        public int Hits;
        /// <summary>
        /// хранит цвет
        /// </summary>
        public Color Color;
        /// <summary>
        /// свойство для отображения
        /// </summary>
        public bool Visible => Hits > 0;
        /// <summary>
        /// конструктор для класса с 3 компонентами
        /// </summary>
        public Brick(Rectangle r, int hits, Color color)
        {
            Rect = r;
            Hits = hits;
            Color = color;
        }
    }
}
