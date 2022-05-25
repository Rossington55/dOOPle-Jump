using System;
using System.Collections.Generic;
using System.Text;

namespace dOOPle_Jump
{
    public class BoxCollider : Collider
    {
        private int _width, _height;
        public override int Top { get { return Y + _height; } }
        public override int Bottom { get { return Y; } }
        public override int Left { get { return X; } }
        public override int Right { get { return X + _width; } }

        public BoxCollider(int x, int y, int w, int h)
        {
            Shape = Shape.Rectangle;

            this.X = x;
            this.Y = y;
            _width = w;
            _height = h;
        }

        public override bool IsCollided(Collider otherCol)
        {
            if(otherCol.Shape == Shape.Rectangle)
            {
                return AABBCollision(this, otherCol as BoxCollider);
            }

            return false;
        }

        public override bool IsInside(Collider otherCol)
        {
            if(otherCol.Shape == Shape.Rectangle)
            {
                return AABBInside(this, otherCol as BoxCollider);
            }

            return false;
        }

    }
}
