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
        private const int SIZE = 20;    // This constant defines the size of the ball

        public Ball(Point position, Point speed, Color color, Graphics graphics, Brush brush, Size clientSize) 
            : base(position, speed, color, graphics, brush, clientSize)
        {
            this.brush = new SolidBrush(color);
        }
       
        public Rectangle GetBounds()    // This method returns the bounds of the ball as a Rectangle object
        {
            return new Rectangle(position.X, position.Y, SIZE, SIZE);
        }

        public void Draw()  // This method draws the ball on the graphics object
        {
            graphics.FillEllipse(brush, new Rectangle(position.X, position.Y, SIZE, SIZE));
        }

        public void Move(bool move) // This method moves the ball by adding the speed to its position if the move parameter is true
        {
            if (move)
            {
                position.X += speed.X;
                position.Y += speed.Y;
            }
        }

        public void ResetBall() // This method resets the position and speed of the ball to a random direction
        {
            Position = new Point(clientSize.Width / 2, clientSize.Height / 2); // Reset position to the center of the client area
            Random random = new Random();

            int directionX = random.Next(0, 2) == 0 ? 5 : -5;   // This generates a random direction for the X-axis speed
            int directionY = random.Next(0, 2) == 0 ? 5 : -5;   // This generates a random direction for the Y-axis speed

            int speedX = random.Next(2, 3) * directionX;        // This generates a random speed for the X-axis, either 2 or 3 times the direction value
            int speedY = random.Next(1, 2) * directionY;        // This generates a random speed for the Y-axis, either 1 or 2 times the direction value

            Speed = new Point(speedX, speedY);
        }

        public void BounceSide() // This method checks if the ball has hit the left or right side of the client area and resets it if it has
        {
            if (position.Y < 0 || position.Y > clientSize.Height)
            {
                speed.Y = -speed.Y;
            }
        }      
    }
}
