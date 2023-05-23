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
        public List<List<List<MapCell>>> Paths;

        public List<MapCell> StartCells { get; private set; }
        public List<MapCell> EndCells { get; private set; }

        public Map(string filePath)
        {
            StartCells = new List<MapCell>();
            EndCells = new List<MapCell>();

            Generate(filePath);
            Paths = new List<List<List<MapCell>>>(StartCells.Count);

            for (var i = 0; i < StartCells.Count; i++) 
            {
                Paths.Add(new List<List<MapCell>>());
                GetPathsFromCell(StartCells[i], new List<MapCell>(), new HashSet<MapCell>(), i);
            }
        }

        public void Generate(string filePath)
        {
            var data = File.ReadAllLines(filePath);
            cells = new MapCell[data.Length, data[0].Length];
            for (var i = 0; i < data.Length; i++)
            {
                for (var j = 0; j < data[i].Length; j++)
                {
                    var cellType = (CellTypes)(data[i][j] - '0');
                    cells[i, j] = new MapCell(cellType, new Rectangle(j * Constans.CellSize, i * Constans.CellSize,
                        Constans.CellSize, Constans.CellSize));
                    if (cellType == CellTypes.StartCell)
                    {
                        StartCells.Add(cells[i, j]);
                    }
                    else if (cellType == CellTypes.EndCell)
                    {
                        EndCells.Add(cells[i, j]);
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
            return InBounds(x, y) ? cells[(int)Math.Floor(y / Constans.CellSize), (int)Math.Floor(x / Constans.CellSize)] : null;
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
            return (x >= 0 && x <= Constans.WindowWidth && y >= 0 && y <= Constans.WindowHeight);
        }

        public MapCell GetNearestPathCell(MapCell cell)
        {
            var direction = ConvertDirectionToInt(cell);
            return GetCellByCoords(cell.Rectangle.Center.X + direction.Item1 * Constans.CellSize,
                cell.Rectangle.Center.Y + direction.Item2 * Constans.CellSize);
        }

        private (int, int) ConvertDirectionToInt(MapCell cell)
        {
            return cell.CellType switch
            {
                CellTypes.PathBottomCell => (0, 1),
                CellTypes.PathTopCell => (0, -1),
                CellTypes.PathLeftCell => (-1, 0),
                CellTypes.PathRightCell => (1, 0),
                _ => ConvertDirectionToInt(GetAllNearestPathCells(cell)[0]),
            };
        }

        private List<MapCell> GetAllNearestPathCells(MapCell cell)
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
                        if (newCell == null) continue;
                        if (newCell.CellType == CellTypes.PathTopCell
                            || newCell.CellType == CellTypes.PathBottomCell
                            || newCell.CellType == CellTypes.PathLeftCell
                            || newCell.CellType == CellTypes.PathRightCell
                            || newCell.CellType == CellTypes.PathRightLeftCell
                            || newCell.CellType == CellTypes.PathRightLeftBottomCell
                            || newCell.CellType == CellTypes.PathTopCell)
                            pathCells.Add(newCell);
                    }
                }
            }

            return pathCells;
        }

        public void GetPathsFromCell(MapCell startCell, List<MapCell> result, HashSet<MapCell> visited, int index)
        {
            var stack = new Stack<MapCell>();
            stack.Push(startCell);

            while (stack.Count != 0)
            {
                var cellToOpen = stack.Pop();
                if (cellToOpen.CellType == CellTypes.EndCell || cellToOpen == null)
                {
                    break;
                }

                result.Add(cellToOpen);
                visited.Add(cellToOpen);

                var nextCell = GetNearestPathCell(cellToOpen);
                if (visited.Contains(nextCell)) continue;
                if (nextCell.CellType == CellTypes.PathRightLeftBottomCell || nextCell.CellType == CellTypes.PathRightLeftCell)
                {
                    GetPathsFromCell(nextCell.CloneWithOtherType(CellTypes.PathLeftCell), 
                        new List<MapCell>(result), new HashSet<MapCell>(visited), index);
                    GetPathsFromCell(nextCell.CloneWithOtherType(CellTypes.PathRightCell),
                        new List<MapCell>(result), new HashSet<MapCell>(visited), index);
                    if (nextCell.CellType == CellTypes.PathRightLeftBottomCell)
                        GetPathsFromCell(nextCell.CloneWithOtherType(CellTypes.PathBottomCell),
                            new List<MapCell>(result), new HashSet<MapCell>(visited), index);
                    return;
                }
                stack.Push(nextCell);
            }

            Paths[index].Add(result);
        }
    }
}