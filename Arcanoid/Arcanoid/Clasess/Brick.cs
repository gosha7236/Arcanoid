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
        public Rectangle Rect;
        public int Hits;
        public Color Color;
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
