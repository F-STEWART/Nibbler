using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    class Caterpillar
    {
        private Queue<Point> Squares;
        public Direction Direction;
        public Grid Grid;
        public IEnumerable<Point> Points;
        public event DeathHandler SelfCollision;
        public delegate void DeathHandler();

        public void Die()
        {
            // Death to the Caterpillar
        }

        public void ChangeDirection(Direction d)
        {
            switch (Direction)
            {
                case Direction.UP:
                    if (!(d==Direction.DOWN))
                    {
                        this.Direction = d;
                    }
                    break;
                case Direction.DOWN:
                    if (!(d == Direction.UP))
                    {
                        this.Direction = d;
                    }
                    break;
                case Direction.LEFT:
                    if (!(d == Direction.RIGHT))
                    {
                        this.Direction = d;
                    }
                    break;
                case Direction.RIGHT:
                    if (!(d == Direction.LEFT))
                    {
                        this.Direction = d;
                    }
                    break;
            }
        }

        public void Update()
        {
            Point newest = Squares.Last();
            switch (Direction)
            {
                case Direction.UP:
                    Squares.Enqueue(new Point(newest.X,newest.Y + 1));
                    break;
                case Direction.DOWN:
                    Squares.Enqueue(new Point(newest.X, newest.Y - 1));
                    break;
                case Direction.LEFT:
                    Squares.Enqueue(new Point(newest.X - 1, newest.Y));
                    break;
                case Direction.RIGHT:
                    Squares.Enqueue(new Point(newest.X + 1, newest.Y));
                    break;
            }
            Squares.Dequeue();
        }

        public void Shrink()
        {
            Squares.Dequeue();
        }

        public void Grow()
        {

        }
    }
}
