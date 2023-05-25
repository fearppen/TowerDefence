using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TowerDefence.AbstractClasses;
using TowerDefence.Controllers;
using TowerDefence.Models;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.States
{
    public class GameState : State
    {
        private int waveNumber;
        private readonly Map map;
        private readonly TowerController towerController;
        private readonly EnemyController enemyController;
        private readonly ProjectileController projectileController;
        private readonly ButtonController buttonController;
        private readonly TextController textController;
        private readonly int levelId;

        public GameState(Game1 game, GraphicsDevice graphics, int levelId) : base(game, graphics)
        {
            GameStats.Healths = Constants.Healths;
            GameStats.Gold = Constants.Gold;
            GameStats.CurrentState = GameStates.Playing;

            this.levelId = levelId;

            map = new Map(string.Format(@"..\..\..\Content\Levels\{0}.txt", levelId));
            towerController = new TowerController();
            projectileController = new ProjectileController();
            buttonController = new ButtonController();
            enemyController = new EnemyController(string.Format(@"..\..\..\Content\Levels\enemies{0}.txt", levelId), map);
            textController = new TextController();

            textController.AddHealthsInfoText();
            textController.AddGoldInfoText();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            towerController.Draw(gameTime, spriteBatch);
            enemyController.Draw(gameTime, spriteBatch);
            projectileController.Draw(gameTime, spriteBatch);
            buttonController.Draw(gameTime, spriteBatch);
            textController.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            TryToPause();
            buttonController.Update(gameTime);
            if (GameStats.Healths <= 0)
            {
                GameStats.CurrentState = GameStates.Intermediate;
                game.ChangeState(new EndLevelState(game, graphicsDevice, levelId, false));
            }

            if (GameStats.CurrentState == GameStates.Playing)
            {
                map.Update(gameTime);
                TowerCellClick();

                if (enemyController.Update(gameTime, waveNumber))
                {
                    if (enemyController.MaxWaves == waveNumber + 1)
                    {
                        GameStats.CurrentState = GameStates.Intermediate;
                        game.ChangeState(new EndLevelState(game, graphicsDevice, levelId, true));
                    }
                    waveNumber++; 
                }

                projectileController.Update(gameTime);
                towerController.Update(gameTime, projectileController, enemyController.Enemies);
                textController.Update(gameTime);
            }
        }


        private void TowerCellClick()
        {
            if (MouseManager.IsClicked())
            {
                var clickedCell = map.GetCellByCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y);
                if (clickedCell.CellType == CellTypes.TowerCell
                    && !towerController.IsTheseTowerCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y))
                {
                    buttonController.AddBuyButton(clickedCell, towerController);
                }

                else if (clickedCell.CellType == CellTypes.TowerCell
                    && towerController.IsTheseTowerCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y))
                {
                    buttonController.AddUpgradeButton(clickedCell, towerController);
                }
            }
        }

        private void TryToPause()
        {
            var currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Escape) && GameStats.CurrentState != GameStates.Paused)
            {
                GameStats.CurrentState = GameStates.Paused;
                buttonController.AddContinueButton();
                buttonController.AddGoToMenuButton(game, graphicsDevice);
            }
        }
    }
}