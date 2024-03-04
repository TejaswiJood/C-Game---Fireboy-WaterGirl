using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBoy_WaterGirl._Models
{
    /// <summary>
    /// This classs is to give a background the the interface
    /// </summary>
    public class BG : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 pos1;


        public BG(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 pos) : base(game)
        {
            this.spriteBatch = spriteBatch;
            
            this.tex = tex;
            this.pos1 = pos;
            

           
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
