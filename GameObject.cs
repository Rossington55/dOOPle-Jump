using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace dOOPle_Jump
{
    public class GameObject
    {
        private Vector2 _pos, _size;
        private Collider _col;

        public GameManager mrBob;
        public Texture2D Sprite;
        public Vector2 Pos { get { return _pos; } set { _pos = value; } }
        public Vector2 ScreenPos
        {
            get
            {
                return new Vector2(_pos.X, Constants.SCREEN_SIZE.Y - _pos.Y - _size.Y);
            }
        }
        public Collider Col { get { return _col; } set { _col = value; } }


        public GameObject(GameManager mrBob, int x, int y, int w, int h)
        {
            _pos = new Vector2(x, y);
            _size = new Vector2(w, h);
            this.mrBob = mrBob;
        }

        public virtual void Update()
        {
            //wrap
            if (_pos.X < 0)
            {
                _pos.X = Constants.SCREEN_SIZE.X;
            }
            else if (_pos.X > Constants.SCREEN_SIZE.X)
            {
                _pos.X = 0;
            }

            if (mrBob.mode == GameMode.Playing)
            {
                _pos.Y -= (int)mrBob.speed;
            }

            _col.X = (int)_pos.X;
            _col.Y = (int)_pos.Y;
        }

        public virtual void Render(SpriteBatch sb)
        {
            sb.Draw(Sprite, ScreenPos, Color.White);
        }
    }
}
