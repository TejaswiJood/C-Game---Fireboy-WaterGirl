using FireBoy_WaterGirl._Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBoy_WaterGirl._Models
{
    public class Lives : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private int lives;


        public Lives(Game game, SpriteBatch spriteBatch, Texture2D tex, int lives) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            
            this.lives = lives;
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            for (int i = 0; i < lives; i++)
            {
                spriteBatch.Draw(tex, new Vector2(100*i +50 , 100), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
