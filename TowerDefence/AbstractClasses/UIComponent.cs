using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TowerDefence.AbstractClasses
{
    public abstract class UIComponent
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract bool Update(GameTime gameTime);
    }
}
