using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Components;
using TowerDefence.Managers;
using TowerDefence.Towers;

namespace TowerDefence
{
    public class GameEngine
    {
        private const int xBuyOffset = 128;
        private const int yBuyOffset = 100;

        public Button GetButtonByCell(MapCell cell)
        {
            var button = new Button(TextureManager.GameButtonTexture, TextureManager.Font)
            {
                Position = new Vector2(cell.Rectangle.Left - xBuyOffset, cell.Rectangle.Top - yBuyOffset),
                Text = "Купить башню"
            };
            return button;
        }

        public GenericTower GetGenericTowerByCell(MapCell cell) 
        {
            var tower = new GenericTower(TextureManager.TowerTexture,
                            new Rectangle(cell.Rectangle.Left - 32, cell.Rectangle.Top - 32,
                            TextureManager.TowerTexture.Width, TextureManager.TowerTexture.Height));
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
    }
}
