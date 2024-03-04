using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FireBoy_WaterGirl._Models
{
    /// <summary>
    /// This is a class to generate the preface we get after we click to play
    /// </summary>
    public class LevelMenu : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D tex;
        private Vector2 pos1, pos2;
        private string msg1;
        private string msg2;
        


        public LevelMenu(Game game, SpriteBatch spriteBatch, SpriteFont font, Texture2D tex, Vector2 pos, string msg1, string msg2) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.tex = tex;
            this.pos1 = pos;
            this.pos2 = new Vector2(pos1.X, pos1.Y + 250);
            

            this.msg1 = msg1;
            this.msg2 = msg2;
            
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.DrawString(font, msg1, new Vector2(pos1.X + (tex.Width - font.MeasureString(msg1).X) / 2, pos1.Y + (tex.Height - font.MeasureString(msg1).Y) / 2), Color.Beige);
            spriteBatch.Draw(tex, pos2, Color.White);
            spriteBatch.DrawString(font, msg2, new Vector2(pos2.X + (tex.Width - font.MeasureString(msg2).X) / 2, pos2.Y + (tex.Height - font.MeasureString(msg2).Y) / 2), Color.NavajoWhite);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

}
