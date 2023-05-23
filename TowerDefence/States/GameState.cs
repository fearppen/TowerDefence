using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Managers;
using TowerDefence.Towers;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence.States
{
    public class GameState : State
    {
        private readonly Map map;
        private readonly List<Tower> towers;
        private readonly EnemyManager enemyManager;
        private readonly List<Projectile> projectiles;
        private List<Component> components;
        private List<Component> pauseComponents;
        private int waveNumber;
        private readonly int levelId;

        public GameState(Game1 game, GraphicsDevice graphics, int levelId) : base(game, graphics)
        {
            this.levelId = levelId;
            map = new Map(string.Format(@"..\..\..\Content\Levels\{0}.txt", levelId));
            components = new List<Component>();
            pauseComponents = new List<Component>();
            towers = new List<Tower>();
            projectiles = new List<Projectile>();
            enemyManager = new EnemyManager(string.Format(@"..\..\..\Content\Levels\enemies{0}.txt", levelId), map);
            GameStats.Healths = Constans.Healths;
            GameStats.Gold = Constans.Gold;
            GameStats.CurrentState = GameStates.Playing;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(Font, string.Format("Золото: {0}", GameStats.Gold), new Vector2(1750, 980), Color.Yellow);
            spriteBatch.DrawString(Font, string.Format("Жизни: {0}", GameStats.Healths), new Vector2(1750, 950), Color.White);
            towers?.ForEach(t => t.Draw(gameTime, spriteBatch));
            enemyManager.Draw(gameTime, spriteBatch);
            projectiles?.ForEach(p => p.Draw(gameTime, spriteBatch));
            components?.ForEach(c => c.Draw(gameTime, spriteBatch));
            pauseComponents?.ForEach(c => c.Draw(gameTime, spriteBatch));
        }

        public override void Update(GameTime gameTime)
        {
            TryToPause();
            if (GameStats.Healths <= 0)
            {
                game.ChangeState(new EndLevelState(game, graphicsDevice, levelId, false));
            }

            if (GameStats.CurrentState == GameStates.Playing)
            {
                map.Update(gameTime);
                TowerCellClick();
                if (enemyManager.Update(gameTime, waveNumber))
                {
                    if (enemyManager.MaxWaves == waveNumber + 1)
                    {
                        game.ChangeState(new EndLevelState(game, graphicsDevice, levelId, true));
                    }
                    waveNumber++; 
                }
                projectiles.RemoveAll(p => p.Update(gameTime));
                components?.RemoveAll(component => component.Update(gameTime));
                towers?.ForEach(t => t.Attack(enemyManager.Enemies, projectiles, gameTime.TotalGameTime.TotalSeconds));
                towers?.RemoveAll(t => t.Update(gameTime));
            }

            else
            {
                pauseComponents.ForEach(c => c.Update(gameTime));
            }
        }


        private void TowerCellClick()
        {
            if (MouseManager.IsClicked())
            {
                var clickedCell = map.GetCellByCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y);
                if (clickedCell.CellType == CellTypes.TowerCell
                    && !map.IsTowerInThisCell(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y, towers))
                {
                    components.Add(GameEngine.GetBuyButton(clickedCell, towers));
                }

                else if (clickedCell.CellType == CellTypes.TowerCell
                    && map.IsTowerInThisCell(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y, towers))
                {
                    var button = GameEngine.GetUpdateButton(clickedCell, towers);
                    if (button != null)
                        components.Add(button);
                }
            }
        }

        private void TryToPause()
        {
            var currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.Escape) && GameStats.CurrentState != GameStates.Paused)
            {
                GameStats.CurrentState = GameStates.Paused;
                components = new ();
                pauseComponents = new List<Component>
                {
                    GameEngine.GetContinueButton(),
                    GameEngine.GetGoToMenuButton(game, graphicsDevice),
                };
            }
            if (GameStats.CurrentState != GameStates.Paused)
                pauseComponents?.RemoveAll(c => true);
        }
    }
}