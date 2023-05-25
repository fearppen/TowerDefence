using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TowerDefence.AbstractClasses;

namespace TowerDefence.Models
{
    public class Background : UIComponent
    {
        private Rectangle rectangle;
        private readonly Texture2D texture;

        public Background(Texture2D texture, Point position, int sizeX, int sizeY)
        {
            this.texture = texture;
            rectangle = new Rectangle(position.X, position.Y, sizeX, sizeY);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public override bool Update(GameTime gameTime) => false;
    }
}
