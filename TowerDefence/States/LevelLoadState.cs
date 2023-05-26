using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;

namespace TowerDefence.States
{
    public class LevelLoadState : State
    {
        private readonly ButtonController buttonController;
        private readonly BackgroundController backgroundController;

        public LevelLoadState(Game1 game, GraphicsDevice graphicsDevice) : base(game, graphicsDevice)
        {
            buttonController = new ButtonController();
            backgroundController = new BackgroundController();

            buttonController.AddLevelSelectButtons(game, graphicsDevice);
            backgroundController.SetLevelLoadBackground();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            backgroundController.Draw(gameTime, spriteBatch);
            buttonController.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            backgroundController.Update(gameTime);
            buttonController.Update(gameTime);
        }
    }
}
