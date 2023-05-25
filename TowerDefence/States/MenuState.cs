using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;

namespace TowerDefence.States
{
    public class MenuState : State
    {
        private readonly ButtonController buttonController;
        private readonly BackgroundController backgroundController;

        public MenuState(Game1 game, GraphicsDevice graphics) : base(game, graphics)
        {
            buttonController = new ButtonController();
            backgroundController = new BackgroundController();

            backgroundController.SetMenuBackground();

            buttonController.AddTestMenuButton(game, graphics);
            buttonController.AddLoadGameMenuButton(game, graphics);
            buttonController.AddExitMenuButton(game);
        }

        public override void Update(GameTime gameTime)
        {
            backgroundController.Update(gameTime);
            buttonController.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            backgroundController.Draw(gameTime, spriteBatch);
            buttonController.Draw(gameTime, spriteBatch);
        }
    }
}
