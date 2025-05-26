using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using task7_graphics;

namespace Pong
{
    public class Paddle : GameObject
    { 
        //private Point position;
        //private Point speed;
        //private Color color;
        //private Graphics graphics;
        //private Brush brush;
        //private Size clientSize;

        //public Point Position { get => position; set => position = value; }
        //public Color Color { get => color; set => color = value; }
        //public Graphics Graphics { get => graphics; set => graphics = value; }
        //public Brush Brush { get => brush; set => brush = value; }
        //public Size ClientSize { get => clientSize; set => clientSize = value; }
        //public Point Speed { get => speed; set => speed = value; }

        public Paddle(Point position, Point speed, Color color, Graphics graphics, Brush brush, Size clientSize) : base(position, speed, color, graphics, brush, clientSize)
        {
            this.color = color;
            this.graphics = graphics;
            this.clientSize = clientSize;
            this.brush = new SolidBrush(color);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(position.X, position.Y, (int)(double)16.5, 95);
        }

        public void Draw()
        {
            graphics.FillRectangle(brush, new Rectangle(position.X, position.Y, (int)(double)16.5, 95));
        }
        
        public void MoveUp(bool moveUp)
        {
            if(moveUp && position.Y > 0)
            {
                position.Y -= speed.Y + (int)(double) 5;
            }
        }
        
        public void MoveDown(bool moveDown)
        {
            if (moveDown && position.Y + GetBounds().Height < clientSize.Height)
            {
                position.Y += speed.Y + (int)(double) 5;
            }
        }
    }
}
