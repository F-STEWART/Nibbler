using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibblerBackEnd
{
    public class Caterpillar
    {
        private Queue<Point> Squares;
        public Direction Direction;
        public Grid Grid;
        public event DeathHandler SelfCollision;
        public delegate void DeathHandler();
        int Grower;

        public void Die()
        {
            this.Squares = new Queue<Point>();
            Squares.Enqueue(new Point(Grid.tiles.GetLength(0/2), Grid.tiles.GetLength(1)/2));
            this.Grower = 0;
        }

        public void WhenSelfCollision()
        {
            OnWhenSelfCollision();
        }
        public void OnWhenSelfCollision()
        {
            if(SelfCollision != null)
            {
                SelfCollision();
            }
        }

        public Caterpillar(Point start, Grid Grid)
        {
            this.Grid = Grid;
            this.Squares = new Queue<Point>();
            Squares.Enqueue(start);
            this.Direction = Direction.RIGHT;
            this.Grower = 0;
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
            Move();
        }

        public Point GetHead()
        {
            return Squares.Last();
        }

        public void Move()
        {
            Point newest = Squares.Last();
            switch (Direction)
            {
                case Direction.UP:
                    grow(new Point(newest.X, newest.Y + 1));
                    break;
                case Direction.DOWN:
                    grow(new Point(newest.X, newest.Y - 1));
                    break;
                case Direction.LEFT:
                    grow(new Point(newest.X - 1, newest.Y));
                    break;
                case Direction.RIGHT:
                    grow(new Point(newest.X + 1, newest.Y));
                    break;
            }
            if (this.Grower <= 0)
            {
                Shrink();
            }
            else
            {
                this.Grower--;
            }
        }

        public void Shrink()
        {
            Squares.Dequeue();
        }

        private void grow(Point next)
        {
            Squares.Enqueue(next);
        }

        public void Grow(int change)
        {
            this.Grower += change;
        }

        public Boolean Contains(Point contents)
        {
            foreach (Point square in Squares)
            {
                if (square == contents)
                    return true;
            }
            return false;
        }
    }
}
