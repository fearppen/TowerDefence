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
        public List<MapCell> StartCell { get; private set; }
        public List<MapCell> EndCell { get; private set; }

        public Map(string filePath, int size)
        {
            Generate(filePath);
        }

        public void Generate(string filePath)
        {
            var data = File.ReadAllLines(filePath);
            cells = new MapCell[data.Length, data[0].Length];
            StartCell = new List<MapCell>();
            EndCell = new List<MapCell>();
            for (var i = 0; i < data.Length; i++) 
            {
                for (var j = 0; j < data[i].Length; j++)
                {
                    var cellType = (CellTypes)(data[i][j] - '0');
                    cells[i, j] = new MapCell(cellType, new Rectangle(j * Constans.CellSize, i * Constans.CellSize, 
                        Constans.CellSize, Constans.CellSize));
                    if (cellType == CellTypes.StartCell)
                    {
                        StartCell.Add(cells[i, j]);
                    }
                    else if (cellType == CellTypes.EndCell)
                    {
                        EndCell.Add(cells[i, j]);
                    }
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
            return InBounds(x, y) ? cells[(int)Math.Floor(y / Constans.CellSize), (int)Math.Floor(x / Constans.CellSize)] : StartCell[0];
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

        public bool InBounds(double x, double y)
        {
            return (x >= 0 && x <= Constans.WindowWidth && y >= 0 && y <= Constans.WindowHight);
        }

        public List<MapCell> GetNearestPathCells(MapCell cell)
        {
            var pathCells = new List<MapCell>();
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if (Math.Abs(dx) + Math.Abs(dy) == 1)
                    {
                        var newCell = GetCellByCoords(cell.Rectangle.Center.X + dx * Constans.CellSize,
                            cell.Rectangle.Center.Y + dy * Constans.CellSize);
                        if (newCell.CellType == CellTypes.PathTopCell
                            || newCell.CellType == CellTypes.PathBottomCell
                            || newCell.CellType == CellTypes.PathLeftCell
                            || newCell.CellType == CellTypes.PathRightCell
                            || newCell.CellType == CellTypes.PathRightLeftCell
                            || newCell.CellType == CellTypes.PathRightLeftBottomCell)
                            pathCells.Add(newCell);
                    }    
                }
            }

            return pathCells;
        }

        public List<MapCell> GetPathFromCell(MapCell cell)
        {
            var path = new List<MapCell>();

            var stack = new Stack<MapCell>();
            stack.Push(cell);
            while (stack.Count > 0)
            {
                var cellToOpen = stack.Pop();
                if (cellToOpen == null || cellToOpen.CellType == CellTypes.EndCell ) 
                    break;
                path.Add(cellToOpen);

                var nextCells = GetNearestPathCells(cellToOpen);
                foreach (var nextCell in nextCells)
                {
                    if (path.Contains(nextCell)) continue;
                    stack.Push(nextCell);
                }
            }

            return path;
        }
    }
}
