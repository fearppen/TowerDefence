using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Managers;
using TowerDefence.Towers;

namespace TowerDefence.States
{
    public class GameState : State
    {
        private Map map;
        private int gold;
        private readonly SpriteFont textFont;
        private readonly List<Component> components;
        private readonly List<MapCell> towerCells;
        private readonly List<Tower> towers;
        private readonly GameEngine engine;
        private readonly EnemyManager enemyManager;

        public GameState(Game1 game, GraphicsDevice graphics, ContentManager content, int levelId) : base(game, graphics, content)
        {
            map = new Map(string.Format(@"..\..\..\Content\Levels\{0}.txt", levelId), 64);
            gold = 100;
            textFont = TextureManager.Font;
            components = new List<Component>();
            towerCells = new List<MapCell>();
            towers = new List<Tower>();
            engine = new GameEngine();
            towerCells = engine.GetAllTowerCells(map);
            enemyManager = new EnemyManager(string.Format(@"..\..\..\Content\Levels\enemies{0}.txt", levelId),
                map.StartCell, map.EndCell);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(textFont, string.Format("Золото: {0}", gold), new Vector2(1700, 50), Color.Yellow);
            components?.ForEach(c => c.Draw(gameTime, spriteBatch));
            towers?.ForEach(t => t.Draw(gameTime, spriteBatch));
            enemyManager.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            TowerCellClick();
            components?.RemoveAll(component => component.Update(gameTime));
            towers?.RemoveAll(t => t.Update(gameTime));
            enemyManager.Update(gameTime);
        }


        private void TowerCellClick()
        {
            if (MouseManager.IsClicked())
            {
                var clickedCell = map.GetCellByCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y);
                if (clickedCell.CellType == CellTypes.TowerCell
                    && !map.IsTowerInThisCell(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y, towers))
                {
                    var buyButton = engine.GetButtonByCell(clickedCell);

                    buyButton.Click += (o, s) =>
                    {
                        var tower = engine.GetGenericTowerByCell(clickedCell);
                        towers.Add(tower);
                        gold -= tower.Cost;
                        buyButton.ClickButton();
                    };
                    components.Add(buyButton);
                }
            }
        }
    }
}