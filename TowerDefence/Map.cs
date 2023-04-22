using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using TowerDefence.Components;
using TowerDefence.Towers;

namespace TowerDefence
{
    public class Map
    {
        public MapCell[,] cells;
        public int CellSize { get; private set; }

        public Map(string filePath, int size)
        {
            CellSize = size;
            Generate(filePath);
        }

        public void Generate(string filePath)
        {
            var data = File.ReadAllLines(filePath);
            cells = new MapCell[data.Length, data[0].Length];
            for (var i = 0; i < data.Length; i++) 
            {
                for (var j = 0; j < data[i].Length; j++)
                {
                    var cellType = (CellTypes)(int)(data[i][j] - '0');
                    cells[i, j] = new MapCell(cellType, new Rectangle(j * CellSize, i * CellSize, CellSize, CellSize));
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var cell in cells)
            {
                cell.Draw(gameTime, spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var cell in cells)
            { 
                cell.Update(gameTime); 
            }
        }

        public MapCell GetCellByCoords(double x, double y)
        {
            return cells[(int)Math.Floor(y / CellSize), (int)Math.Floor(x / CellSize)];
        }

        public bool IsTowerInThisCell(double x, double y, List<Tower> towers)
        {
            var rectangle = new Rectangle((int)x, (int)y, 1, 1);
            foreach (var tower in towers)
            {
                if (tower.Rectangle.Intersects(rectangle))
                    return true;
            }
            return false;
        }
    }
}
