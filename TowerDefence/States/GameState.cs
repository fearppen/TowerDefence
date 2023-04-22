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

        public GameState(Game1 game, GraphicsDevice graphics, ContentManager content, int levelId) : base(game, graphics, content)
        {
            map = new Map(string.Format(@"..\..\..\Content\Levels\{0}.txt", levelId), 64);
            gold = 100;
            textFont = TextureManager.Font;
            towerCells = GetAllTowerCells();
            components = new List<Component>();
            towerCells = new List<MapCell>();
            towers = new List<Tower>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            map.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(textFont, string.Format("Золото: {0}", gold), new Vector2(1700, 50), Color.Yellow);
            components?.ForEach(c => c.Draw(gameTime, spriteBatch));
            towers?.ForEach(t => t.Draw(gameTime, spriteBatch));
        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            TowerCellClick();
            components?.ForEach(component => component.Update(gameTime));
            towers?.ForEach(t => t.Update(gameTime));
        }

        private List<MapCell> GetAllTowerCells()
        {
            var cells = new List<MapCell>();
            foreach (var cell in map.cells)
            {
                if (cell.CellType == CellTypes.TowerCell)
                    cells.Add(cell);
            }
            return cells;
        }

        private void TowerCellClick()
        {
            if (MouseManager.IsClicked())
            {
                var clickedCell = map.GetCellByCoords(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y);
                if (clickedCell.CellType == CellTypes.TowerCell
                    && !map.IsTowerInThisCell(MouseManager.CurrentMouse.X, MouseManager.CurrentMouse.Y, towers))
                {
                    var buyButton = new Button(TextureManager.GameButtonTexture, TextureManager.Font)
                    {
                        Position = new Vector2(clickedCell.Rectangle.Left - 128, clickedCell.Rectangle.Top - 100),
                        Text = "Купить башню"
                    };

                    buyButton.Click += (o, s) =>
                    {
                        var tower = new GenericTower(TextureManager.TowerTexture, 
                            new Rectangle(clickedCell.Rectangle.Left - 32, clickedCell.Rectangle.Top - 32, 
                            TextureManager.TowerTexture.Width, TextureManager.TowerTexture.Height));
                        towers.Add(tower);
                        gold -= tower.Cost;
                        //components.Clear();
                    };
                    components.Add(buyButton);
                }
            }
        }
    }
}