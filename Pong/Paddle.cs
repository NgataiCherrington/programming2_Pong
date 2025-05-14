using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class Paddle
    {
        private Point position;
        private Color color;
        private Graphics graphics;
        private Brush brush;
        private Size clientSize;

        public Point Position { get => position; set => position = value; }
        public Color Color { get => color; set => color = value; }
        public Graphics Graphics { get => graphics; set => graphics = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Size ClientSize { get => clientSize; set => clientSize = value; }

        public Paddle(Point position, Color color, Graphics graphics, Brush brush, Size clientSize) 
        {
            this.Position = position;
            this.Color = color;
            this.Graphics = graphics;
            this.Brush = brush;
            this.ClientSize = clientSize;
            brush = new SolidBrush(color);
        }

        public void Draw()
        {
            graphics.FillRectangle(brush, new Rectangle(position.X, position.Y, 15, 75));
        }


    }


}
