using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Pong;

namespace task7_graphics
{
    public class Ball : GameObject
    {
        private const int SIZE = 20;     
        
        public static int SIZE1 => SIZE;
       
        public Ball(Point position, Point speed, Color color, Graphics graphics, Brush brush, Size clientSize) 
            : base(position, speed, color, graphics, brush, clientSize)
        {
            this.brush = new SolidBrush(color);
        }
       
        public Rectangle GetBounds()
        {
            return new Rectangle(position.X, position.Y, SIZE, SIZE);
        }

        public void Draw()
        {
            graphics.FillEllipse(brush, new Rectangle(position.X, position.Y, SIZE, SIZE));
        }

        public void Move(bool move)
        {
            if (move)
            {
                position.X += speed.X;
                position.Y += speed.Y;
            }
        }

        public void ResetBall()
        {
            Position = new Point(ClientSize.Width / 2, ClientSize.Height / 2);

            Random random = new Random();

            int directionX = random.Next(0, 2) == 0 ? 5 : -5;
            int directionY = random.Next(0, 2) == 0 ? 5 : -5;

            int speedX = random.Next(2, 3) * directionX;
            int speedY = random.Next(1, 2) * directionY;

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
