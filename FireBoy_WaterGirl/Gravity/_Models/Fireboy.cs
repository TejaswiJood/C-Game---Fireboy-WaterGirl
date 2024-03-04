using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using FireBoy_WaterGirl._Manager;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FireBoy_WaterGirl._Models
{

    public class Fireboy 
    {
        // Initilizing the variables
        private const float SPEED = 450f;
        private float GRAVITY = 7000f;
        private const float JUMP = 1500f;
        private const int OFFSET = 10;
        private Vector2 _velocity;
        private bool _onGround;
        private bool soundPlayed = false;
        private GameManager _gameManager;
        private Game1 _game;
        private Texture2D _texture;
        private Vector2 _position;
        int animationFrameTimer;
        private int _level;

        public Fireboy(Game1 game, Texture2D texture, Vector2 position, GameManager gameManager, int level)
        {
            _game = game;
            _gameManager = gameManager;
            _texture = texture;
            _position = position;
            _level = level;

        }

        private Rectangle CalculateBounds(Vector2 pos)
        {
            return new((int)pos.X + OFFSET, (int)pos.Y, _texture.Width - (2 * OFFSET), _texture.Height);
        }

        /// <summary>
        /// This will update the directing in which to move and at what speed
        /// This will only happen if the player has not reached the final position
        /// </summary>
        private void UpdateVelocity()
        {
            animationFrameTimer += (int)(Globals.Time*100);

            List<Texture2D> _texturesRight = new List<Texture2D>();
            _texturesRight.Add(Globals.Content.Load<Texture2D>("FBoyRight"));
            _texturesRight.Add(Globals.Content.Load<Texture2D>("FBoyRight1"));
            List<Texture2D> _texturesLeft = new List<Texture2D>();
            _texturesLeft.Add(Globals.Content.Load<Texture2D>("FBoyLeft"));
            _texturesLeft.Add(Globals.Content.Load<Texture2D>("FBoyLeft1"));

            var keyboardState = Keyboard.GetState();
            if (stop == false)
            {
                if (keyboardState.IsKeyDown(Keys.A)) 
                { _velocity.X = -SPEED;
                    int frameIndex = (animationFrameTimer / 10) % _texturesLeft.Count;

                    _texture = _texturesLeft[frameIndex];

                    if (animationFrameTimer >= 10 * _texturesLeft.Count)
                    {
                        animationFrameTimer = 0;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.D)) 
                {
                    _velocity.X = SPEED;

                    int frameIndex = (animationFrameTimer / 10) % _texturesRight.Count;

                    _texture = _texturesRight[frameIndex];

                    if (animationFrameTimer >= 10 * _texturesRight.Count)
                    {
                        animationFrameTimer = 0;
                    }

                }
                else { _velocity.X = 0; ; _texture = Globals.Content.Load<Texture2D>("FBoy"); }

                    _velocity.Y += GRAVITY * Globals.Time;

                if (keyboardState.IsKeyDown(Keys.W) && _onGround)
                {
                    _velocity.Y = -JUMP;
                }
            }
            else
            {
                _velocity.X = 0;
                _velocity.Y = 0;

            }
        }


        /// <summary>
        /// This is to update the position of the player(FireBoy) according to the speed
        /// </summary>
        private void UpdatePosition()
        {
            _onGround = false;
            var newPos = _position + (_velocity * Globals.Time);
            Rectangle newRect = CalculateBounds(newPos);
            if (_level == 1)
            {
                foreach (var collider in Map.GetNearestColliders(newRect))
                {
                    if (newPos.X != _position.X)
                    {
                        newRect = CalculateBounds(new(newPos.X, _position.Y));
                        if (newRect.Intersects(collider))
                        {
                            if (newPos.X > _position.X) newPos.X = collider.Left - _texture.Width + OFFSET;
                            else newPos.X = collider.Right - OFFSET;
                            continue;
                        }
                    }

                    newRect = CalculateBounds(new(_position.X, newPos.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (_velocity.Y > 0)
                        {
                            newPos.Y = collider.Top - _texture.Height;
                            _onGround = true;
                            _velocity.Y = 0;
                            //effect.Play();
                        }
                        else
                        {
                            newPos.Y = collider.Bottom;
                            _velocity.Y = 0;
                        }
                    }

                }
            }
            else
            {
                foreach (var collider in Map2.GetNearestColliders(newRect))
                {
                    if (newPos.X != _position.X)
                    {
                        newRect = CalculateBounds(new(newPos.X, _position.Y));
                        if (newRect.Intersects(collider))
                        {
                            if (newPos.X > _position.X) newPos.X = collider.Left - _texture.Width + OFFSET;
                            else newPos.X = collider.Right - OFFSET;
                            continue;
                        }
                    }

                    newRect = CalculateBounds(new(_position.X, newPos.Y));
                    if (newRect.Intersects(collider))
                    {
                        if (_velocity.Y > 0)
                        {
                            newPos.Y = collider.Top - _texture.Height;
                            _onGround = true;
                            _velocity.Y = 0;
                        }
                        else
                        {
                            newPos.Y = collider.Bottom;
                            _velocity.Y = 0;
                        }
                    }

                }
            }
            _position = newPos;
        }




        /// <summary>
        /// This is to check if the player is in collision of acid or water
        /// This will update the death if it happens
        /// </summary>
        /// <param name="effect"></param>
        private void UpdateDeath(SoundEffect effect)
        {
            // _onGround = false;
            var newPos = _position;
            Rectangle newRect = CalculateBounds(newPos);
            if(_level == 1) {
                foreach (var collider in Map.GetNearestAcids(newRect))
                {
                    if (newRect.Intersects(collider))
                    {

                        if (!soundPlayed)
                        {
                            effect.Play();
                            soundPlayed = true;
                            _gameManager.RestartGame();
                            _gameManager.Over();
                            _game.ChangeGameState(Game1.GameState.Pause);

                        }
                    }
                }
                
            }
            else
            {
                foreach (var collider in Map2.GetNearestAcids(newRect))
                {
                    if (newRect.Intersects(collider))
                    {

                        if (!soundPlayed)
                        {
                            effect.Play();
                            soundPlayed = true;
                            _gameManager.RestartGame();
                            _gameManager.Over();
                            _game.ChangeGameState(Game1.GameState.Pause);

                        }
                    }
                }
                foreach (var collider in Map2.GetNearestWater(newRect))
                {
                    if (newRect.Intersects(collider))
                    {

                        if (!soundPlayed)
                        {
                            effect.Play();
                            soundPlayed = true;
                            _gameManager.RestartGame();
                            _gameManager.Over();
                            _game.ChangeGameState(Game1.GameState.Pause);

                        }
                    }
                }
            }
            
            _position = newPos;
        }
        bool stop = false;


        /// <summary>
        /// Will update the Finish of the player
        /// </summary>
        private void UpdateFinish()
        {
            var newPos = _position;
            Rectangle newRect = CalculateBounds(newPos);
            if (_level == 1)
            {
                foreach (var collider in Map.GetNearestFinisher(newRect))
                {
                    if (newRect.Intersects(collider))
                    {

                        if (!soundPlayed)
                        {
                            //effect.Play();
                            soundPlayed = true;
                            stop = true;

                            _gameManager.FinishFireBoy();

                        }
                    }
                }
            }
            else
            {
                foreach (var collider in Map2.GetNearestFinisher(newRect))
                {
                    if (newRect.Intersects(collider))
                    {

                        if (!soundPlayed)
                        {
                            //effect.Play();
                            soundPlayed = true;
                            stop = true;

                            _gameManager.FinishFireBoy();
                        }
                    }
                }
            }
            
            _position = newPos;
        }


        public void Update(SoundEffect effect)
        {
            UpdateVelocity();
            UpdatePosition();
            UpdateDeath(effect);
            UpdateFinish();
        }
        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, _position, Color.White);

        }
    }
}