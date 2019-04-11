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

        public void WhenSelfCollision()
        {
            OnWhenSelfCollision();
        }

        // Raise SelfCollision Event
        public void OnWhenSelfCollision()
        {
            if(SelfCollision != null)
            {
                SelfCollision();
            }
        }

        // Resets position of caterpillar
        public void Die()
        {
            //Place first point in middle of board
            int MiddleX = this.Grid.tiles.GetLength(0) / 2;
            int MiddleY = this.Grid.tiles.GetLength(1) / 2;
            Point start = new Point(MiddleX, MiddleY);
            // Place tail directly to the right of that
            Point tail = new Point(start.X - 1, start.Y);
            // Create queue and enqueue both points
            this.Squares = new Queue<Point>();
            Squares.Enqueue(tail);
            Squares.Enqueue(start);
            this.Grower = 0;
            //Instantiate direction to be right
            this.Direction = Direction.RIGHT;
        }

        // Instantiates Caterpillar
        public Caterpillar(Point start, Grid Grid)
        {
            Point tail = new Point(start.X-1, start.Y);
            this.Grid = Grid;
            this.Squares = new Queue<Point>();
            Squares.Enqueue(tail);
            Squares.Enqueue(start);
            this.Direction = Direction.RIGHT;
            this.Grower = 0;
        }

        // Does not allow the direction to be changed to the one directly oposite to where you are pointing
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

        // Moves Caterpillar and checks for Self Collision
        public void Update()
        {
            Move();
            int Count = 1;
            foreach (Point X in Squares)
            {
                if(X == GetHead() && Count != GetLength())
                {
                    WhenSelfCollision();
                }
                Count++;
            }
        }

        // Returns length of caterpillar
        public int GetLength()
        {
            return Squares.Count;
        }

        // Returns Head of caterpillar
        public Point GetHead()
        {
            return Squares.Last();
        }

        // Returns Tail of caterpillar
        public Point GetTail()
        {
            return Squares.First();
        }

        // Get point right after tail
        public Point GetTailLeader()
        {
            foreach (Point point in Squares)
            {
                if (point != GetTail())
                    return point;
            }
            return new Point(0,0);
        }

        // Finds point after a given point
        public Point GetLeader(Point subject)
        {
            Boolean next = false;
            foreach (Point point in Squares)
            {
                if (next)
                    return point;
                else if (subject == point)
                    next = true;
            }
            return new Point(0, 0);
        }

        // Moves the queue forward by one (Grows from the back if necesary)
        public void Move()
        {
            Point newest = GetHead();
            switch (Direction)
            {
                case Direction.UP:
                    grow(new Point(newest.X, newest.Y - 1));
                    break;
                case Direction.DOWN:
                    grow(new Point(newest.X, newest.Y + 1));
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

        // Removes last element of queue
        public void Shrink()
        {
            Squares.Dequeue();
        }

        // Adds a point to the queue
        private void grow(Point next)
        {
            Squares.Enqueue(next);
        }

        // Adds a number to the grower
        public void Grow(int change)
        {
            this.Grower += change;
        }

        // checks to see if a point is contained in the queue
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
