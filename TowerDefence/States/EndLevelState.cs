using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;

namespace TowerDefence.States
{
    public class EndLevelState : State
    {
        private readonly ButtonController buttonController;
        private readonly BackgroundController backgroundController;
        private readonly TextController textController;

        public EndLevelState(Game1 game, GraphicsDevice graphics, int levelId, bool isWon) : base(game, graphics)
        {
            buttonController = new ButtonController();
            backgroundController = new BackgroundController();
            textController = new TextController();

            backgroundController.SetEndLevelBackground();
            buttonController.InitEndLevel(game, graphics, levelId);

            textController.InitLevelEndText(isWon);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            backgroundController.Draw(gameTime, spriteBatch);
            buttonController.Draw(gameTime, spriteBatch);
            textController.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            backgroundController.Update(gameTime);
            buttonController.Update(gameTime);
            textController.Update(gameTime);
        }
    }
}
