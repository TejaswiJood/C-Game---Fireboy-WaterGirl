using FireBoy_WaterGirl._Models;
using FireBoy_WaterGirl._Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Microsoft.Xna.Framework.Audio;

// Game Name - Fireboy & WaterGirl

// Makers 
// Tejaswi Singh Jood



namespace FireBoy_WaterGirl
{
    public class Game1 : Game
    {
        // Describing various variables name we will be needing while the making of the app
        private GraphicsDeviceManager _graphics;
        private Texture2D btnTex;
        private Vector2 btnPos;
        private MouseState ms;
        SoundEffect effect;
        GameManager _gameManager;
        SpriteFont font, font1;
        SpriteBatch spriteBatch;
        private float currentTime = 0f;
        private Score score;
        BG bg;
        BG background;
        private int i = 0;
        private int level;


        // The set of variousmode we can acces while running the program and initilizing the mode to Load
        public enum GameState {Load, Main, Play, Help, About, Won, Pause, Exit,Levels};
        public GameState state = GameState.Load;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void ChangeGameState(GameState newState)
        {
            state = newState;
            LoadContent();
        }
        protected override void Initialize()
        {
            Globals.WindowSize = new(Map2.tiles1.GetLength(1) * Map2.TILE_SIZE, Map2.tiles1.GetLength(0) * Map2.TILE_SIZE);
            _graphics.PreferredBackBufferWidth = Globals.WindowSize.X;
            _graphics.PreferredBackBufferHeight = Globals.WindowSize.Y;
            _graphics.ApplyChanges();

            Globals.Content = Content;
            base.Initialize();
        }

        /// <summary>
        /// This step is to load the content accoring to the gamestate we are in.
        /// This gamestate can be predefined or can even be input bu the user.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Song backgroundMusic = this.Content.Load<Song>("Sound/bgm");
            MediaPlayer.IsRepeating = true;
            Song backgroundMusic1 = this.Content.Load<Song>("Sound/bgmplay");          
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.GraphicsDevice = GraphicsDevice;
            font = Content.Load<SpriteFont>("file");
            font1 = Content.Load<SpriteFont>("Font1");
            
            

            if(state == GameState.Levels)
            {
                Texture2D bg = this.Content.Load<Texture2D>("Images/BGI");
                BG background = new BG(this, spriteBatch, bg, Vector2.Zero);
                this.Components.Add(background);
                
                btnTex = this.Content.Load<Texture2D>("Images/button2");
                btnPos = new Vector2(600, 350);
                LevelMenu btns = new LevelMenu(this, spriteBatch, font, btnTex, btnPos, "Level 1", "Level 2");
                this.Components.Add(btns);
            }
            if (state == GameState.Main)
            {
                Texture2D bg = this.Content.Load<Texture2D>("Images/BGI");
                BG background = new BG(this, spriteBatch, bg, Vector2.Zero);
                this.Components.Add(background);
                btnTex = this.Content.Load<Texture2D>("Images/button2");
                btnPos = new Vector2(600, 150);
                Menu btns = new Menu(this, spriteBatch, font, btnTex, btnPos, "Play", "Help", "About","Exit");
                this.Components.Add(btns);                
            }
            else if(state == GameState.Pause)
            {                
                Texture2D pause = this.Content.Load<Texture2D>("Images/pausemenu");
                string message = "LEVEL FAILED !!! \n\n SCORE - "+(currentTime*3).ToString("0");
                GameOver go = new GameOver(this, spriteBatch, font, pause, new Vector2(0,0),message);
                this.Components.Add(go);
            }
            else if (state == GameState.Won)
            {
                Texture2D pause = this.Content.Load<Texture2D>("Images/pausemenu");
                string message = "Congratulations !!! \n" +
                    "YOU WON!\n SCORE - " + (currentTime * 3).ToString("0");
                GameOver go = new GameOver(this, spriteBatch, font, pause, new Vector2(0, 0), message);
                this.Components.Add(go);
            }
            else if (state == GameState.Play)
            {
                
                currentTime = 0;
                score = new Score(this, spriteBatch, font, Vector2.Zero, "Score", Color.Black);
                this.Components.Add(score);
                _gameManager = new GameManager(this, level);
                
                _gameManager.Draw(level);


            }
            else if(state == GameState.Load)
            {
                Texture2D bg = this.Content.Load<Texture2D>("Images/BG1");
                BG background = new BG(this, spriteBatch, bg, Vector2.Zero);
                this.Components.Add(background);
                MediaPlayer.Stop();
                // MediaPlayer.Play(backgroundMusic);

            }
            else if (state == GameState.Help)
            {
                
                Texture2D bg = this.Content.Load<Texture2D>("Images/Play1");
                BG background = new BG(this, spriteBatch, bg, Vector2.Zero);
                this.Components.Add(background);
            }

            else if (state == GameState.About)
            {
                Texture2D bg = this.Content.Load<Texture2D>("Images/BGI");
                BG background = new BG(this, spriteBatch, bg, Vector2.Zero);
                this.Components.Add(background);
                string aboutMe = "Fireboy & WaterGirl :  \n\n It is a simple multiplayer game. There are two characters Fireboy \n and Watergirl they have to escape from the Dungeon. \n There is acid on the floor, they have to reach the exit gate\n without touching the acid.\n\n\n Maker of the Game - Tejaswi Singh Jood \n\n\n\n\n\n\n                                press M to go back.....";
                Message aboutMessage = new Message(this, spriteBatch, font1, aboutMe, Color.Black);
                this.Components.Add(aboutMessage);

         
            }
            else if(state == GameState.Exit)
            {
                Exit();
            }
        }

        /// <summary>
        /// This will update the page realtime accoring to the game state the user is in.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            effect = Globals.Content.Load<SoundEffect>("Sound/beep");

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.M))
            {
                effect.Play();

                this.Components.Clear();
                state = GameState.Main;
                LoadContent();
            }
            ms = Mouse.GetState();

            if(state == GameState.Load)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    effect.Play();

                    this.Components.Clear();
                    state = GameState.Main;
                    LoadContent();
                }
            }
            if(state == GameState.Pause)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Vector2 position = new Vector2(ms.X, ms.Y);

                    // Play button
                    if (position.X >= 630 && position.X <= 1150 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Play;
                        LoadContent();
                    }
                    // help button
                    else if (position.X >= 1150 && position.X <= 1390 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Exit;
                        LoadContent();
                    }
                    //about button
                    else if (position.X >= 100 && position.X <= 340 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Main;
                        LoadContent();
                    }                    
                }
            }
            if (state == GameState.Won)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Vector2 position = new Vector2(ms.X, ms.Y);

                    // Play button
                    if (position.X >= 630 && position.X <= 1150 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Play;
                        LoadContent();
                    }
                    // help button
                    else if (position.X >= 1150 && position.X <= 1390 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Exit;
                        LoadContent();
                    }
                    //about button
                    else if (position.X >= 100 && position.X <= 340 && position.Y >= 870 && position.Y <= 970)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Main;
                        LoadContent();
                    }
                }
            }
            if (state == GameState.Help)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Vector2 position = new Vector2(ms.X, ms.Y);

                    // Play button
                    if (position.X >= 700 && position.X <= 700 + btnTex.Width+50 && position.Y >= 1000 && position.Y <= 1000 + btnTex.Height+50)
                    {
                        effect.Play();
                        this.Components.Clear();
                        state = GameState.Main;
                        LoadContent();
                    }
                }
            }
            if(state == GameState.Play)
            {
                
                Vector2 d = font.MeasureString(score.message);
                Vector2 ssPos = new Vector2((_graphics.PreferredBackBufferWidth - d.X) /2,10);
                score.pos = ssPos;
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(currentTime > 59)
                {
                    currentTime = 0;
                    i++;
                }
                score.message = "Timer ( "+i+ " : "+ currentTime.ToString("00")+")";
                Draw(gameTime);
                
                
                _gameManager.Update(level);


               
            }
            if(state == GameState.Levels)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Vector2 position = new Vector2(ms.X, ms.Y);

                    // Play button
                    if (position.X >= 600 && position.X <= 600 + btnTex.Width && position.Y >= 350 && position.Y <= 350 + btnTex.Height)
                    {
                        effect.Play();
                        level = 1;
                        this.Components.Clear();
                        state = GameState.Play;
                        LoadContent();
                    }
                    // help button
                    else if (position.X >= 600 && position.X <= 600 + btnTex.Width && position.Y >= 600 && position.Y <= 600 + btnTex.Height)
                    {
                        effect.Play();
                        level= 2;
                        this.Components.Clear();
                        state = GameState.Play;
                        LoadContent();
                    }
                } 
            }

            if (state == GameState.Main)
                {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Vector2 position = new Vector2(ms.X, ms.Y);

                    // Play button
                    if (position.X >= 600 && position.X <= 600 + btnTex.Width && position.Y >= 150 && position.Y <= 150 + btnTex.Height)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Levels;
                        LoadContent();
                    }
                    // help button
                    else if (position.X >= 600 && position.X <= 600 + btnTex.Width  && position.Y >= 400 && position.Y <= 400 + btnTex.Height)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Help;
                        LoadContent();
                    }
                    //about button
                    else if (position.X >= 600 && position.X <= 600 + btnTex.Width && position.Y >= 650 && position.Y <= 650 + btnTex.Height)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.About;
                        LoadContent();
                    }
                    //exit button
                    else if (position.X >= 600 && position.X <= 600 + btnTex.Width && position.Y >= 900 && position.Y <= 900 + btnTex.Height)
                    {
                        effect.Play();

                        this.Components.Clear();
                        state = GameState.Exit;
                        LoadContent();
                    }
                }
            }
            Globals.Update(gameTime);
            base.Update(gameTime);
        } 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            if (state == GameState.Play)
            {
               
                Texture2D bgTexture = this.Content.Load<Texture2D>("Tiles/Lvl1");
                spriteBatch.Begin();
                spriteBatch.Draw(bgTexture, Vector2.Zero, Color.Honeydew);
                spriteBatch.End();
                _gameManager.Draw(level);
                
            }

            base.Draw(gameTime);
        }
    }
}