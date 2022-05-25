using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace dOOPle_Jump
{
    public class Player : GameObject
    {

        private const float SPEED = 300;
        private const float CATCH_TOLERANCE = 20;
        private Vector2 _vel;
        private bool _standing = true;


        public Player(GameManager mrBob, int x, int y, int w, int h) : base(mrBob, x, y, w, h)
        {
            _vel = Vector2.Zero;
            Col = new BoxCollider(x, y, w, h);
        }

        public void Move()
        {
            Pos += _vel * mrBob.deltaT;

            if (!_standing)
            {
                _vel.Y -= 11f;
            }
        }


        public void Jump()
        {
            if(mrBob.mode == GameMode.Init)
            {
                mrBob.mode = GameMode.Playing;
                mrBob.difficulty = Speed.Slow;
            }

            _standing = false;
            _vel.Y = SPEED * 1.5f;
        }

        public override void Update()
        {
            //COLLISION DETECTION
            //Only check collisions on the way down
            if (_vel.Y <= 0)
            {
                //Check for collisions
                if (mrBob.IsPlayerCollided(out Collider col))
                {
                    //Within catching range
                    if (Col.Bottom > col.Top - CATCH_TOLERANCE)
                    {
                        //Place on top of platform
                        Vector2 pos = Pos;
                        pos.Y = col.Top;
                        _vel.Y = 0;
                        Pos = pos;

                        _standing = true;
                    }
                    else
                    {
                        _standing = false;
                    }
                }
                else
                {
                    _standing = false;
                }
            }

            //LEFT & RIGHT MOVEMENT
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) { _vel.X = SPEED; }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left)) { _vel.X = -SPEED; }
            else { _vel.X = 0; }

            //JUMPING
            if (_standing && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Jump();
            }
            

            Move();

            base.Update();
            

        }
    }
}
