using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace task7_graphics
{
    public class Ball
    {
        private const int SIZE = 20;
        private Point speed;
        private Point position;
        private Color color;
        private Graphics graphics;
        private Brush brush;
        private Size clientSize;

        public static int SIZE1 => SIZE;
        public Point Speed { get => speed; set => speed = value; }
        public Point Position { get => position; set => position = value; }
        public Color Color { get => color; set => color = value; }
        public Graphics Graphics { get => graphics; set => graphics = value; }
        public Brush Brush { get => brush; set => brush = value; }
        public Size ClientSize { get => clientSize; set => clientSize = value; }

        public Ball(Point speed, Point position, Color color, Graphics graphics, Size clientSize)
        {
            this.Speed = speed;
            this.Position = position;
            this.Color = color;
            this.Graphics = graphics;
            this.ClientSize = clientSize;
            brush = new SolidBrush(color);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(position.X, position.Y, SIZE, SIZE);
        }

        public void Draw()
        {
            graphics.FillEllipse(brush, new Rectangle(position.X, position.Y, SIZE, SIZE));
        }

        public void Move()
        {
            position.X += speed.X;
            position.Y += speed.Y;
        }

        public void ResetBall()
        {
            Position = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

            Random random = new Random();

            int directionX = random.Next(0, 2) == 0 ? 5 : -5;
            int directionY = random.Next(0, 2) == 0 ? 5 : -5;

            int speedX = random.Next(4, 7) * directionX;
            int speedY = random.Next(2, 5) * directionY;

            Speed = new Point(speedX, speedY);
        }

        public void BounceSide()
        {
            if (position.X < 0 || position.X > clientSize.Width)
            {
                ResetBall();
            }

            if (position.Y < 0 || position.Y > clientSize.Height)
            {
                speed.Y = -speed.Y;
            }
        }

       
    }
}
