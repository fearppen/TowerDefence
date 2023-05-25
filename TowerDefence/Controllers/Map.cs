using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using TowerDefence.AbstractClasses;
using TowerDefence.Models;
using TowerDefence.StaticClasses;

using static TowerDefence.StaticClasses.TextureManager;

namespace TowerDefence.Controllers
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
                    cells[i, j] = new MapCell(cellType, new Rectangle(j * Constants.CellSize, i * Constants.CellSize,
                        Constants.CellSize, Constants.CellSize), GetTextureByCellType(cellType));

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
            return InBounds(x, y) ? cells[(int)Math.Floor(y / Constants.CellSize), (int)Math.Floor(x / Constants.CellSize)] : null;
        }

        public bool InBounds(double x, double y)
        {
            return x >= 0 && x <= Constants.WindowWidth && y >= 0 && y <= Constants.WindowHeight;
        }

        public MapCell GetNearestPathCell(MapCell cell)
        {
            var direction = ConvertDirectionToInt(cell);
            return GetCellByCoords(cell.Rectangle.Center.X + direction.Item1 * Constants.CellSize,
                cell.Rectangle.Center.Y + direction.Item2 * Constants.CellSize);
        }

        private Texture2D GetTextureByCellType(CellTypes cellType)
        {
            if (cellType == CellTypes.EmptyCell)
            {
                return GrassTexture;
            }

            else if (cellType == CellTypes.TowerCell)
            {
                return TowerBlockTexture;
            }
            return WallTexture;
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
                        var newCell = GetCellByCoords(cell.Rectangle.Center.X + dx * Constants.CellSize,
                            cell.Rectangle.Center.Y + dy * Constants.CellSize);
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