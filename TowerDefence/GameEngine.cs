using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TowerDefence.Components;
using TowerDefence.Managers;
using TowerDefence.Towers;
using static TowerDefence.Managers.TextureManager;

namespace TowerDefence
{
    public class GameEngine
    {
        private const int xBuyOffset = 128;
        private const int yBuyOffset = 100;

        public Button GetButtonByCell(MapCell cell)
        {
            var button = new Button(GameButtonTexture, Font)
            {
                Position = new Vector2(cell.Rectangle.Left - xBuyOffset, cell.Rectangle.Top - yBuyOffset),
                Text = "Купить башню"
            };
            return button;
        }

        public GenericTower GetGenericTowerByCell(MapCell cell) 
        {
            var tower = new GenericTower(TowerTexture,
                            new Rectangle(cell.Rectangle.Left - 32, cell.Rectangle.Top - 32,
                            TowerTexture.Width, TowerTexture.Height));
            return tower;
        }

        public List<MapCell> GetAllTowerCells(Map map)
        {
            var cells = new List<MapCell>();
            foreach (var cell in map.cells)
            {
                if (cell.CellType == CellTypes.TowerCell)
                    cells.Add(cell);
            }
            return cells;
        }

        public DelayedAction GetDelayedActionEnemy(Dictionary<char, Dictionary<string, int>> enemyStats,
            char enemyType, MapCell startCell, List<MapCell> path, int delay, List<Enemy.Enemy> enemies)
        {
            var texture = enemyType switch
            {
                'v' => new List<Texture2D>() { VampireTexture },
                'o' => new List<Texture2D>() { OrkWalkTexture },
                _ => new List<Texture2D>() { VampireTexture },
            };

            return new DelayedAction(() => enemies.Add(new Enemy.Enemy(enemyStats[enemyType]["maxHp"],
                enemyStats[enemyType]["speed"], texture, new Point(startCell.Rectangle.Left, startCell.Rectangle.Top + 32), path)),
                delay);
        }
    }
}
