using FireBoy_WaterGirl._Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireBoy_WaterGirl._Manager
{
    public class GameManager
    {
        // Initializing the variables 
        private readonly Map _map;
        private readonly Map2 _map2;

        private Fireboy _hero;
        private Watergirl _girl;
        private SoundEffect _effect;

        // To check if the player has reached the fininsh line
        private bool boyfinish = false;
        private bool girlfinish = false;

        int lives = 4;
        private int _level;
        private Game1 _game;

        public GameManager(Game1 game, int level)
        {
            _level = level;
            _game = game;            
            _map = new();            
            _map2 = new();            
            _hero = new Fireboy(_game,Globals.Content.Load<Texture2D>("FBoy"), new(Globals.WindowSize.X * 0.04f, 1150), this, _level);
            _girl = new Watergirl(_game,Globals.Content.Load<Texture2D>("WGirl"), new(Globals.WindowSize.X * 0.95f, 1150), this,_level); 

            // effect for player when they die
            _effect = Globals.Content.Load<SoundEffect>("Sound/D");
        }

        /// <summary>
        /// Updating the maps and the players{Fireboy and Watergirl}
        /// </summary>
        /// <param name="level"></param>
        public void Update(int level)
        {
            _hero.Update(_effect);
            _girl.Update(_effect);         
            _map.Update();          
            _map2.Update();           

        }

        public void Draw(int level)
        {
            Globals.SpriteBatch.Begin();
            _hero.Draw();
            _girl.Draw();
            // Drawing the map according to what level was called
            if (level == 1)
            {
                _map.Draw();
            }
            else
            {
                _map2.Draw();
            }           
            Globals.SpriteBatch.End();
        }

        /// <summary>
        /// Restarting the level
        /// This will include to creating players with a new startingposition
        /// </summary>
        public void RestartGame()
        {
            _hero = new Fireboy(_game,Globals.Content.Load<Texture2D>("FBoy"), new Vector2(Globals.WindowSize.X *0.04f,1150), this, _level);
            _girl = new Watergirl(_game, Globals.Content.Load<Texture2D>("WGirl"), new Vector2(Globals.WindowSize.X * 0.95f, 1150), this,_level);            
            girlfinish = false;
            boyfinish = false;
        }


        /// <summary>
        /// Will be called when the FireBoy / WaterGirl finishes
        /// </summary>
        public void FinishWaterGirl()
        {
            girlfinish = true;
            Finish();            
        }
        public void FinishFireBoy()
        {
            boyfinish = true;
            Finish();
        }
         

        /// <summary>
        /// Will be called when both of the players finishes
        /// </summary>
        private void Finish()
        {
            if(boyfinish == true && girlfinish == true)
            {
                _game.ChangeGameState(Game1.GameState.Won);
            }
        }



        public int Over()
        {
            lives--;
            return lives;
        }
    }
}
