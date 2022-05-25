using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace dOOPle_Jump.Platforms
{
    class BasicPlatform : GameObject
    {

        public BasicPlatform(GameManager mrBob, int x, int y, int w, int h) : base(mrBob, x, y, w, h)
        {
            Col = new BoxCollider(x, y, w, h);
        }
    }
}
