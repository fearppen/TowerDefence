using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.AbstractClasses
{
    public abstract class State
    {
        protected Game1 game;
        protected GraphicsDevice graphicsDevice;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
        }
    }
}
