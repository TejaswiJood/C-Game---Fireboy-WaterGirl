using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireBoy_WaterGirl._Models
{
    /// <summary>
    /// This class is th update and write the score when the user is playing the game
    /// </summary>
    public class Score : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        public Vector2 pos;
        public string message;
        private Color color;

    
        public Score(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 pos, string message, Color color) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.pos = pos;
            this.message = message;
            this.color = color;
        }

    
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, pos, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    
    }
}
