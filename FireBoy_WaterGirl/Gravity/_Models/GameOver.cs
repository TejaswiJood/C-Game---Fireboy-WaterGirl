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
    /// This preface we get after either dying or winnig the level
    /// </summary>
    public class GameOver : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D tex;
        private Vector2 pos1;
        private string msg1;

        public GameOver(Game game,SpriteBatch spriteBatch, SpriteFont font, Texture2D tex, Vector2 pos1, string msg1) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.tex = tex;
            this.pos1 = pos1;
            this.msg1 = msg1;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.DrawString(font, msg1, new Vector2(590,550), Color.IndianRed);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
