using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBoy_WaterGirl._Models
{
 
    /// <summary>
    /// This class is to print the menu 
    /// </summary>
    public class Menu : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Texture2D tex;
        private Vector2 pos1, pos2, pos3, pos4;
        private string msg1;
        private string msg2;
        private string msg3;
        private string msg4;

        
        public Menu(Game game, SpriteBatch spriteBatch, SpriteFont font, Texture2D tex, Vector2 pos, string msg1, string msg2, string msg3, string msg4) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.tex = tex;
            this.pos1 = pos;
            this.pos2 = new Vector2(pos1.X, pos1.Y+ 250);
            this.pos3 = new Vector2(pos1.X, pos1.Y+500);
            this.pos4 = new Vector2(pos1.X, pos1.Y + 750);
            
            this.msg1 = msg1;
            this.msg2 = msg2;
            this.msg3 = msg3;
            this.msg4 = msg4;
        }

        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, pos1, Color.White);
            spriteBatch.DrawString(font, msg1, new Vector2(pos1.X + (tex.Width - font.MeasureString(msg1).X)/2, pos1.Y + (tex.Height - font.MeasureString(msg1).Y) / 2), Color.Beige);
            spriteBatch.Draw(tex, pos2, Color.White);
            spriteBatch.DrawString(font, msg2, new Vector2(pos2.X + (tex.Width - font.MeasureString(msg2).X)/2, pos2.Y + (tex.Height - font.MeasureString(msg2).Y) / 2), Color.NavajoWhite);
            spriteBatch.Draw(tex, pos3, Color.White);
            spriteBatch.DrawString(font, msg3, new Vector2(pos3.X + (tex.Width - font.MeasureString(msg3).X) / 2, pos3.Y + (tex.Height - font.MeasureString(msg3).Y) / 2), Color.NavajoWhite);
            spriteBatch.Draw(tex, pos4, Color.White);
            spriteBatch.DrawString(font, msg4, new Vector2(pos4.X + (tex.Width - font.MeasureString(msg4).X) / 2, pos4.Y + (tex.Height - font.MeasureString(msg4).Y) / 2), Color.NavajoWhite);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
