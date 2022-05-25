using dOOPle_Jump.Platforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace dOOPle_Jump
{
    public enum GameMode { Init, Playing, Paused, GameOver };
    public enum Speed { Stopped, Slow, Medium, Fast };

    public class GameManager
    {
        private Player _player;
        private List<GameObject> _platforms;
        private ContentManager _content;
        private SpriteFont _font;
        private int _lastPlatform;
        private int _currentPlatform;
        private int _score = 0;
        private int _maxPlatformDist = 7;
        
        public Speed difficulty;
        public Speed speed;
        public GameMode mode;
        public float deltaT;


        public GameManager()
        {
            _player = new Player(this, 0, 100, 100, 100);
            _platforms = new List<GameObject>();
            difficulty = Speed.Stopped;
            speed = difficulty;
            mode = GameMode.Init;
        }

        public void Init(ContentManager content)
        {
            _font = content.Load<SpriteFont>("VCR OSD Mono");

            _player.Sprite = content.Load<Texture2D>("Player");
            _content = content;

            //Floor
            _platforms.Add(new BasicPlatform(this, 0, 0, 720, 100));
            _platforms[0].Sprite = content.Load<Texture2D>("Ground");

            //Add initial platforms
            do { } while (GenerateNewPlatforms());
        }

        public bool IsPlayerCollided(out Collider col)
        {
            //Check every platform to see if there's a collision
            foreach (GameObject platform in _platforms)
            {
                if (platform.Col.IsCollided(_player.Col))
                {
                    col = platform.Col;
                    _currentPlatform = _platforms.FindIndex(a => a == platform);
                    return true;
                }
            }

            col = null;
            return false;
        }

        private bool GenerateNewPlatforms()
        {
            GameObject lastPlatform = _platforms[_platforms.Count - 1];
            //If space for a new platform
            if (lastPlatform.ScreenPos.Y > 0)
            {
                //X = make sure at least half of platform is on screen
                //Y = somewhere within 9 platforms tall

                Random rand = new Random();
                GameObject newPlatform = new BasicPlatform(this,
                    rand.Next((int)Constants.SCREEN_SIZE.X - (int)Constants.PLATFORM_SIZE.X / 2),//x
                    rand.Next(_maxPlatformDist * (int)Constants.PLATFORM_SIZE.Y) + (int)lastPlatform.Pos.Y + (int)Constants.PLATFORM_SIZE.Y,//y
                    (int)Constants.PLATFORM_SIZE.X,//w
                    (int)Constants.PLATFORM_SIZE.Y//h
                    );

                newPlatform.Sprite = _content.Load<Texture2D>("Platform");
                _platforms.Add(newPlatform);
                return true;
            }
            return false;
        }

        public void Update()
        {

            switch (mode)
            {
                case GameMode.Init:
                    _player.Update();
                    break;
                case GameMode.Playing:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        mode = GameMode.Paused;
                    }

                    //Dont let player reach top of screen
                    if (_player.ScreenPos.Y < Constants.SCREEN_SIZE.Y / 7)//If in top 7th of screen
                    {
                        speed = Speed.Fast;
                    }
                    else
                    {
                        speed = difficulty;
                    }

                    _player.Update();

                    GenerateNewPlatforms();

                    //Unload platforms below screen
                    _platforms.RemoveAll(platform =>
                    {
                        return platform.Col.Top < 0;
                    });

                    //Update all platforms
                    _platforms.ForEach(platform =>
                    {
                        platform.Update();
                    });

                    //HANDLE SCORING
                    //Check if player is on a new platform
                    if (_currentPlatform != _lastPlatform)
                    {
                        _score++;
                        _lastPlatform = _currentPlatform;
                    }
                    switch (difficulty)
                    {
                        case Speed.Slow:
                            if (_score > 20)
                            {
                                difficulty = Speed.Medium;
                            }
                            break;
                        case Speed.Medium:
                            if (_score > 40)
                            {
                                difficulty = Speed.Fast;
                            }
                            break;
                    }

                    //Check for death
                    if (_player.Col.Top < 0)
                    {
                        mode = GameMode.GameOver;
                    }
                    break;
                case GameMode.Paused:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        mode = GameMode.Playing;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Render(SpriteBatch sb)
        {

            _platforms.ForEach(obj =>
            {
                obj.Render(sb);
            });

            _player.Render(sb);


            //GUI TEXT
            switch (mode)
            {
                case GameMode.Playing:
                    sb.DrawString(_font,
                                  $"Current Score: {_score}",
                                  new Vector2(10, 10),
                                  Color.Black)
                        ;
                    break;
                case GameMode.Paused:
                    sb.DrawString(_font,
                                  "Paused",
                                  new Vector2(10, 10),
                                  Color.Black)
                        ;
                    break;
                case GameMode.GameOver:
                    // Game Over text
                    string gameOverTxt = "GAME OVER";
                    Vector2 goStrSize = _font.MeasureString(gameOverTxt);
                    sb.DrawString(_font,
                                  gameOverTxt,
                                  new Vector2(
                                        Constants.SCREEN_SIZE.X / 2 - goStrSize.X / 2,
                                        Constants.SCREEN_SIZE.Y / 2 - goStrSize.Y / 2
                                   ),
                                  Color.Red
                    );

                    //Final score text
                    string scoreTxt = $"Score: {_score}";
                    Vector2 scoreStrSize = _font.MeasureString(scoreTxt);
                    sb.DrawString(_font,
                                  scoreTxt,
                                  new Vector2(
                                        Constants.SCREEN_SIZE.X / 2 - scoreStrSize.X / 2,
                                        Constants.SCREEN_SIZE.Y / 2 - scoreStrSize.Y / 2 + goStrSize.Y
                                   ),
                                  Color.Red
                    );
                    break;
            }
        }
    }
}
