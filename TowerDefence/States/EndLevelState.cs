using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;
using TowerDefence.StaticClasses;

namespace TowerDefence.States
{
    public class EndLevelState : State
    {
        private readonly string text;
        private readonly Vector2 position;
        private readonly ButtonController buttonController;
        private readonly BackgroundController backgroundController;
        private readonly TextController textController;

        public EndLevelState(Game1 game, GraphicsDevice graphics, int levelId, bool isWon) : base(game, graphics)
        {
            buttonController = new ButtonController();
            backgroundController = new BackgroundController();
            textController = new TextController();

            backgroundController.SetEndLevelBackground();
            buttonController.AddGoToMenuButton(game, graphics);
            buttonController.AddRestartLevelButton(game, graphics, levelId);

            text = isWon ? "Вы победили! Сыграйте снова или попробуйте другой уровень" : "Вы проиграли. Попробуйте сыграть снова!";
            var width = isWon ? Constants.WindowWidth / 3.6f : Constants.WindowWidth / 2.78f;
            position = new Vector2(width, Constants.WindowHeight / 10);
            textController.AddEndLevelMessage(text, position);
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
