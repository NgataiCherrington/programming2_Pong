using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    /// <summary>
    /// Base class for all game objects in the Pong game
    /// </summary>
    public class GameObject
    {
        protected Point position;
        protected Point speed;
        protected Color color;
        protected Graphics graphics;
        protected Brush brush;
        protected Size clientSize;

        protected GameObject(Point position, Point speed, Color color, Graphics graphics, Brush brush, Size clientSize)
        {
            Position = position;
            Speed = speed;
            Color = color;
            Graphics = graphics;
            ClientSize = clientSize;
            brush = new SolidBrush(color);
        }

        public Point Position { get => position; set => position = value; }
        public Point Speed { get => speed; set => speed = value; }
        public Color Color { get => color; set => color = value; }
        public Graphics Graphics { get => graphics; set => graphics = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Size ClientSize { get => clientSize; set => clientSize = value; }
    }
}
