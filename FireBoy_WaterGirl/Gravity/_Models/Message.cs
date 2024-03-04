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
    /// Message class to show string to help page and about page
    /// </summary>
    public class Message : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Vector2 pos;
        private string message;
        private Color color;

        /// <summary>
        /// constructor for saving info to variables
        /// </summary>
        public Message(Game game, SpriteBatch spriteBatch, SpriteFont font, string message, Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.pos = new Vector2(300, 360);
            this.message = message;
            this.color = color;
        }

        /// <summary>
        /// draw string with message
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, pos, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
