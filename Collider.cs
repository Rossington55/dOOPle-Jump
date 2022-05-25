using System;
using System.Collections.Generic;
using System.Text;

namespace dOOPle_Jump
{
    public enum Shape { Rectangle, Circle}
    public abstract class Collider
    {
        private int _x, _y;

        public Shape Shape;
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }
        public abstract int Top { get; }
        public abstract int Bottom { get; }
        public abstract int Left { get; }
        public abstract int Right { get; }

        public Collider() { }

        //Touching at all (including deges)
        public abstract bool IsCollided(Collider otherCol);


        //Full internal collision
        public abstract bool IsInside(Collider otherCol);

        public bool AABBCollision(BoxCollider a, BoxCollider b)
        {
            //Bounding box collision detection
            if (a.Bottom > b.Top) { return false; }
            if (a.Left > b.Right) { return false; }
            if (a.Right < b.Left) { return false; }
            if (a.Top < b.Bottom) { return false; }
            return true;
        }

        public bool AABBInside(BoxCollider a, BoxCollider b)
        {

            //Bounding box collision detection
            if (a.Bottom >= b.Top) { return false; }
            if (a.Left >= b.Right) { return false; }
            if (a.Right <= b.Left) { return false; }
            if (a.Top <= b.Bottom) { return false; }
            return true;
        }

        //Add circle on circle
        //Add circle on rect
    }
}
