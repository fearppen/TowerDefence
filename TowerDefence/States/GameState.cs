using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Win32;

namespace TowerDefence.States
{
    public class GameState : State
    {
        private Map map;

        public GameState(Game1 game, GraphicsDevice graphics, ContentManager content, int levelId) : base(game, graphics, content) 
        {
            map = new Map(@"..\..\..\Content\Levels\test.txt", 64);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            map.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
