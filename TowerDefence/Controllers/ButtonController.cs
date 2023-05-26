using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.AbstractClasses;
using TowerDefence.Models;
using TowerDefence.States;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
{
    public class ButtonController
    {
        public const int XBuyOffset = -128;
        public const int YBuyOffset = -100;

        public const int LevelsCount = 3;
        public const int DistanceBetweenLevelButtons = 30;

        private readonly List<UIComponent> playComponents;
        private readonly List<UIComponent> pauseComponents;

        public ButtonController()
        {
            playComponents = new List<UIComponent>();
            pauseComponents = new List<UIComponent>();
        }

        public void Update(GameTime gameTime)
        {
            if (GameStats.CurrentState != GameStates.Paused)
            {
                pauseComponents.Clear();
                playComponents.RemoveAll(c => c.Update(gameTime));
            }
            else
            {
                playComponents.Clear();
                pauseComponents.ForEach(c => c.Update(gameTime));
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            playComponents?.ForEach(c => c.Draw(gameTime, spriteBatch));
            pauseComponents?.ForEach(c => c.Draw(gameTime, spriteBatch));
        }

        public void AddGoToMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var button = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constants.WindowHeight / 2 + ButtonMenuTexture.Height))
            {
                Text = "Выйти в меню",
            };

            button.Click += (o, s) => {
                GameStats.CurrentState = GameStates.Intermediate;
                game.ChangeState(new MenuState(game, graphics));
            };

            if (GameStats.CurrentState == GameStates.Paused)
                pauseComponents.Add(button);
            else
                playComponents.Add(button);
        }

        public void AddContinueButton()
        {
            var button = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2, 
                Constants.WindowHeight / 2 - ButtonMenuTexture.Height))
            {
                Text = "Продолжить игру",
            };

            button.Click += (o, s) => { GameStats.CurrentState = GameStates.Playing; };

            pauseComponents.Add(button);
        }

        public void AddRestartLevelButton(Game1 game, GraphicsDevice graphics, int levelId)
        {
            var button = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2, 
                Constants.WindowHeight / 2 - ButtonMenuTexture.Height))
            {
                Text = "Перезапустить уровень",
            };

            button.Click += (o, s) => { game.ChangeState(new GameState(game, graphics, levelId)); };
            playComponents.Add(button);
        }

        public void AddLoadGameMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var loadGameButton = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2,
                Constants.WindowHeight / 2 - ButtonMenuTexture.Height * 1.5f))
            {
                Text = "Уровни"
            };

            loadGameButton.Click += (o, s) => { game.ChangeState(new LevelLoadState(game, graphics)); };
            playComponents.Add(loadGameButton);
        }

        public void AddExitMenuButton(Game1 game)
        {
            var quitGameButton = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2, 
                Constants.WindowHeight / 2 + ButtonMenuTexture.Height))
            {
                Text = "Выход"
            };

            quitGameButton.Click += (o, s) => { game.Exit(); };

            playComponents.Add(quitGameButton);
        }

        public void AddTestMenuButton(Game1 game, GraphicsDevice graphics)
        {
            var testGameButton = new Button(ButtonMenuTexture, Font, new Vector2(Constants.WindowWidth / 2 - ButtonMenuTexture.Width / 2, 
                Constants.WindowHeight / 2 - ButtonMenuTexture.Height / 3)) 
            {
                Text = "Тест" 
            };

            testGameButton.Click += (o, s) => { game.ChangeState(new GameState(game, graphics, 0)); };

            playComponents.Add(testGameButton);
        }

        public void AddBuyButton(MapCell cell, TowerController towerController)
        {
            var button = new Button(GameButtonTexture, Font, new Vector2(cell.Rectangle.Left + XBuyOffset,
                cell.Rectangle.Top + YBuyOffset))
            {
                Text = string.Format("Купить башню {0}", Constants.TowerCost)
            };

            button.Click += (o, s) =>
            {
                towerController.AddGenericTowerInCell(cell);
                button.ClickButton();
            };

            playComponents.Add(button);
        }

        public void AddUpgradeButton(MapCell cell, TowerController towerController)
        {
            var cost = towerController.GetTowerCost(cell);
            if (!towerController.CanUpgradeTowerInCell(cell))
                return;

            var button = new Button(GameButtonTexture, Font, new Vector2(cell.Rectangle.Left + XBuyOffset,
                cell.Rectangle.Top + YBuyOffset))
            {
                Text = string.Format("Улучшить {0}", cost)
            };

            button.Click += (o, s) =>
            {
                towerController.UpgradeTowerInCell(cell);
                button.ClickButton();
            };

            playComponents.Add(button);
        }

        public void AddLevelSelectButtons(Game1 game, GraphicsDevice graphics)
        {
            var countButtonsInRow = Constants.WindowWidth / 
                (LevelSelectButtonTexture.Width + DistanceBetweenLevelButtons);

            var countButtonsInColumn = Constants.WindowHeight /
                (LevelSelectButtonTexture.Height + DistanceBetweenLevelButtons);

            var j = 0;

            for (var i = 0; i < LevelsCount; i++)
            {
                var levelId = i;
                var pos = new Vector2(DistanceBetweenLevelButtons 
                    + (i % (countButtonsInRow) * (LevelSelectButtonTexture.Width + DistanceBetweenLevelButtons)),
                    DistanceBetweenLevelButtons + j * (LevelSelectButtonTexture.Height + DistanceBetweenLevelButtons));

                var button = new Button(LevelSelectButtonTexture, Font, pos)
                {
                    Text = string.Format("{0}", i + 1)
                };

                button.Click += (o, s) =>
                {
                    game.ChangeState(new GameState(game, graphics, levelId + 1));
                };

                playComponents.Add(button);

                if (i % countButtonsInRow == countButtonsInRow - 1)
                {
                    j++;
                }
            }
        }

        public void DeleteAllGameButtons()
        {
            playComponents.Clear();
        }
    }
}
